using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models.DataAccessPostgreSqlProvider;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Storage;

namespace WebApplication.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoUpload(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                var xs = new XmlSerializer(typeof(StorageModel));
                var storage = (StorageModel) xs.Deserialize(stream);


                using (var db = new StorageDbContext())
                {
                    var dbs = new DbStorage()
                    {
                        Name = storage.Name,
                        Capacity = storage.Capacity,
                        Address = storage.Address,
                        StorageType = storage.StorageType,
                    };
                    dbs.Items = new Collection<DbItem>();
                    foreach (var item in storage.Items)
                    {
                        dbs.Items.Add(new DbItem()
                        {
                            Name = item.Name,
                            Price = item.Price,
                            Units = item.Units,
                            Manufacturer = item.Manufacturer
                        });
                    }
                    dbs.Workers = new Collection<DbWorkers>();
                    foreach (var worker in storage.Workers)
                    {
                        dbs.Workers.Add(new DbWorkers()
                        {
                            Name = worker.Name,
                            Position = worker.Position,
                            Experience = worker.Experience,
                        });
                    }
                    db.StorageFacilities.Add(dbs);
                    db.SaveChanges();
                }

                return View(storage);
            }
        }

       

        public ActionResult List()
        {
            List<DbStorage> list;
            using (var db = new StorageDbContext())
            {
                list = db.StorageFacilities.Include(s => s.Workers).ToList();
                list = db.StorageFacilities.Include(s=>s.Items).ToList();
            }

            return View(list);
        }


        public ActionResult Print(int id)
        {
            using (var db = new StorageDbContext())
            {
                var storage = db.StorageFacilities
                    .Include(s1 => s1.Items)
                    .First(s1=>s1.Id == id);
                var storage1 = db.StorageFacilities
                    .Include(s1 => s1.Workers)
                    .First(s1 => s1.Id == id);

                IWorkbook workbook =
                    new XSSFWorkbook(System.IO.File.OpenRead("template.xlsx"));

                var sheet = workbook.GetSheetAt(0);
                sheet.GetRow(1).Cells[0].SetCellValue(storage.Workers[0].Name);
                sheet.GetRow(1).Cells[1].SetCellValue(storage.Address);
                sheet.GetRow(1).Cells[2].SetCellValue(storage.Capacity);
                sheet.GetRow(1).Cells[3].SetCellValue(storage.StorageType.ToString());
                for (int i = 5; i <= sheet.LastRowNum; i++) 
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue;
                    if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                    var lastCellNum = row.LastCellNum;
                    for (int j = row.FirstCellNum; j < lastCellNum; j++)
                    {
                        var cell = row.GetCell(j);
                        if (cell != null)
                        {
                            if (cell.StringCellValue == "$Item")
                            {
                                int b = 0;
                                foreach (var item in storage.Items)
                                {
                                    row = sheet.GetRow(i);
                                    cell = row.GetCell(j);
                                    cell.SetCellValue(item.Name);
                                    cell = row.GetCell(j + 1) ?? row.CreateCell(j + 1);
                                    cell.SetCellValue(item.Price);
                                    cell = row.GetCell(j + 2) ?? row.CreateCell(j + 2);
                                    cell.SetCellValue(item.Units);
                                    cell = row.GetCell(j + 3) ?? row.CreateCell(j + 3);
                                    cell.SetCellValue(item.Manufacturer);
                                    if(b < storage.Workers.Count())
                                    {
                                        cell = row.GetCell(j + 5);
                                        cell.SetCellValue(storage.Workers[b].Name);
                                        cell = row.GetCell(j + 6) ?? row.CreateCell(j + 6);
                                        cell.SetCellValue(storage.Workers[b].Position);
                                        cell = row.GetCell(j + 7) ?? row.CreateCell(j + 7);
                                        cell.SetCellValue(storage.Workers[b].Experience);
                                    }
                                    if (item != storage.Items.Last())
                                    {
                                        row = sheet.CopyRow(i, i + 1);
                                    }
                                    i++;
                                    b++;
                                }
                                break;
                            }
                        }
                    }
                }

                var ms = new MemoryStream();
                workbook.Write(ms);

                ms.Position = 0;

                return base.File(ms, "application/octet-stream", "storage" + storage.Name + ".xlsx");
            }
        }
    }
}