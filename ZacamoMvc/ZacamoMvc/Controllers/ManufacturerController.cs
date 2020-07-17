using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities;
using ZacamoMvc.ManufacturerServiceReference;


namespace ZacamoMvc.Controllers
{

    [Authorize(Roles = "Admin"), HandleError]
    public class ManufacturerController : Controller
    {
        private ManufacturerServiceClient manufacturerRepository;

        public ManufacturerController()
        {
            manufacturerRepository = new ManufacturerServiceClient();
        }

        private List<Manufacturer> ManufacturerDtosToManufacturers(List<ManufacturerDto> manufacturerDtos)
        {
            List<Manufacturer> manufacturers = manufacturerDtos.Select(m => new Manufacturer()
            {
                ManufacturerId = m.ManufacturerId,
                Name = m.Name,
                Website = m.Website
            }).ToList();

            return manufacturers;
        }

        private Manufacturer ManufacturerDtoToManufacturer(ManufacturerDto manufacturerDto)
        {
            Manufacturer manufacturer = new Manufacturer()
            {
                ManufacturerId = manufacturerDto.ManufacturerId,
                Name = manufacturerDto.Name,
                Website = manufacturerDto.Website
            };
            return manufacturer;
        }

        // GET: Manufacturer
        public ActionResult Index()
        {
            List<ManufacturerDto> manufacturersDtos = manufacturerRepository.GetAllManufacturers();
            List<Manufacturer> manufacturers = ManufacturerDtosToManufacturers(manufacturersDtos);
            return View(manufacturers);
        }

        // GET: Manufacturer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manufacturer/Create
        [HttpPost]
        public ActionResult Create(Manufacturer manufacturer)
        {
            try
            {
                manufacturerRepository.AddManufacturer(manufacturer);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Manufacturer/Edit/5
        public ActionResult Edit(int id)
        {
            ManufacturerDto manufacturerDto = manufacturerRepository.GetManufacturerById(id);

            if (manufacturerDto == null)
            {
                return RedirectToAction("PageNotFound", "Error");
            }

            Manufacturer manufacturer = ManufacturerDtoToManufacturer(manufacturerDto);
            return View(manufacturer);
        }

        // POST: Manufacturer/Edit/5
        [HttpPost]
        public ActionResult Edit(Manufacturer manufacturer)
        {
            try
            {
                manufacturerRepository.UpdateManufacturer(manufacturer);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Manufacturer/Delete/5
        public ActionResult Delete(int id)
        {
            ManufacturerDto manufacturerDto = manufacturerRepository.GetManufacturerById(id);

            if (manufacturerDto == null)
            {
                return RedirectToAction("PageNotFound", "Error");
            }

            Manufacturer manufacturer = ManufacturerDtoToManufacturer(manufacturerDto);
            return View(manufacturer);
        }

        // POST: Manufacturer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Manufacturer manufacturer)
        {
            try
            {
                string result = manufacturerRepository.DeleteManufacturer(id);

                if (result == "Manufacturer has products so can not be deleted")
                {
                    ManufacturerDto manufacturerObjDto = manufacturerRepository.GetManufacturerById(id);
                    Manufacturer manufacturerObj = ManufacturerDtoToManufacturer(manufacturerObjDto);
                    ModelState.AddModelError("", $"{manufacturerObj.Name} can not be deleted as there are products associated with it");
                    return View(manufacturerObj);
                }

                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                ManufacturerDto manufacturerObjDto = manufacturerRepository.GetManufacturerById(id);
                Manufacturer manufacturerObj = ManufacturerDtoToManufacturer(manufacturerObjDto);
                ModelState.AddModelError("", $"{manufacturerObj.Name} can not be deleted as there are products associated with it");
                return View(manufacturerObj);
            }
            catch
            {
                ManufacturerDto manufacturerObjDto = manufacturerRepository.GetManufacturerById(id);
                Manufacturer manufacturerObj = ManufacturerDtoToManufacturer(manufacturerObjDto);
                return View(manufacturerObj);
            }
        }
    }
}
