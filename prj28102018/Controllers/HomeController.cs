using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prj28102018.Models;

namespace prj28102018.Controllers
{
    public class HomeController : Controller
    {
        PrjMngEntities _db = new PrjMngEntities();
        public ActionResult Index()
        {
            var result = _db.t_UserInfo.ToList();

            return View();
        }
        public ActionResult Index1()
        {
          //  var result = _db.t_UserInfo.ToList();

            return View();
        }
        public JsonResult GetEmpDetails(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            
            try
            {
                List<t_UserInfo> lstMarks = new List<t_UserInfo>();
                lstMarks = _db.t_UserInfo.ToList();
                switch (jtSorting)
                {
                    case "RollNumber ASC":
                        lstMarks = lstMarks.OrderBy(t => t.UserId).ToList();
                        break;
                    case "RollNumber DESC":
                        lstMarks = lstMarks.OrderByDescending(t => t.UserId).ToList();
                        break;
                    case "Name ASC":
                        lstMarks = lstMarks.OrderBy(t => t.EmpName).ToList();
                        break;
                    case "Name DESC":
                        lstMarks = lstMarks.OrderByDescending(t => t.EmpName).ToList();
                        break;
                    case "MarksObtained ASC":
                        lstMarks = lstMarks.OrderBy(t => t.EmpPhone).ToList();
                        break;
                    case "MarksObtained DESC":
                        lstMarks = lstMarks.OrderByDescending(t => t.EmpPhone).ToList();
                        break;
                }

                lstMarks = lstMarks.Skip(jtStartIndex).Take(jtPageSize).ToList();
                int TotalRecords = _db.t_UserInfo.Count();
                return Json(new { Result = "OK", Records = lstMarks, TotalRecordCount = TotalRecords });
                //List<t_UserInfo> lstEmpDetails = new List<t_UserInfo>();
                //lstEmpDetails = _db.t_UserInfo.ToList();
                //return Json(new { Result = "OK", Records = lstEmpDetails }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        //[HttpGet]
        //public JsonResult StudentList()
        //{
        //    try
        //    {
        //        //Get data from database
        //        // int studentCount = _repository.StudentRepository.GetStudentCount();
        //        List<t_UserInfo> EmpList = _db.t_UserInfo.ToList();

        //        //Return result to jTable
        //      // return Json(new { Result = "OK", Records = EmpList });

        //        return this.Json(new { Result = "OK", Records = EmpList, JsonRequestBehavior.AllowGet });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}















        public ActionResult LoadData()
        {
            try
            {
                //Creating instance of DatabaseContext class  
                using (PrjMngEntities _context = new PrjMngEntities())
                {
                    var draw = Request.Form.GetValues("draw").FirstOrDefault();
                    var start = Request.Form.GetValues("start").FirstOrDefault();
                    var length = Request.Form.GetValues("length").FirstOrDefault();
                    var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                    var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                    var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();


                    //Paging Size (10,20,50,100)    
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    //int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;

                    // Getting all Customer data    
                    var customerData = (from tempcustomer in _context.t_UserInfo
                                        select tempcustomer);

                    //Sorting    
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                       // customerData = customerData.OrderBy(sortColumn + " " + sortColumnDir);
                    }
                    //Search    
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        customerData = customerData.Where(m => m.UserId == searchValue);
                    }

                    //total number of rows count     
                    recordsTotal = customerData.Count();
                    //Paging     
                    var data = customerData.ToList();
                    //Returning Json Data    
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

    }

}
