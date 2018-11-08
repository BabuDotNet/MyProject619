using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using prj28102018.Models;
using System.Linq.Dynamic;
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


        //public virtual PartialViewResult Menu()
        //{
        //    //IEnumerable<Menu> Menu = null;

        //    //if (Session["_Menu"] != null)
        //    //{
        //    //    Menu = (IEnumerable<Menu>)Session["_Menu"];
        //    //}
        //    //else
        //    //{
        //    //    Menu = MenuData.GetMenus("10001");// pass employee id here
        //    //    Session["_Menu"] = Menu;
        //    //}
        //    //return PartialView("_Menu", Menu);
        //}



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
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;

                    // Getting all Customer data    
                    var customerData = (from t_UserInfo in _context.t_UserInfo
                                        select t_UserInfo);

                    //Sorting    
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        customerData = customerData.OrderBy(sortColumn + " " + sortColumnDir);
                    }
                    //Search    
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        customerData = customerData.Where(m => m.EmpName.ToLower().Contains(searchValue)
                        || m.EmpPhone.ToLower().Contains(searchValue) || m.EmployeeID.ToLower().Contains(searchValue));
                        //              customerData= customerData.AsEnumerable()
                        //.Where(r => r.Field<String>("FIRSTNAME").Contains(searchValue)
                        //       || r.Field<String>("LASTNAME").Contains(searchValue))
                        //       || r.Field<String>("EmpName").Contains(searchValue)
                        //       || r.Field<String>("COMPANY").Contains(searchValue)
                        //       || r.Field<String>("CREATOR").Contains(searchValue));

                    }

                    //total number of rows count     
                    recordsTotal = customerData.Count();
                    //Paging     
                    var data = customerData.Skip(skip).Take(pageSize).ToList();
                    //Returning Json Data    
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                }
            }
            catch (Exception)
            {
                throw;
            }

        }




        private t_UserInfo MaptoModel(t_UserInfo assetVM)
        {
            t_UserInfo asset = new t_UserInfo()
            {
                EmailID = assetVM.EmailID,
                EmpName = assetVM.EmpName,
                EmpAdrs = assetVM.EmpAdrs,
                EmpPhone = assetVM.EmpPhone,
                UserId = assetVM.UserId,
                UsrPwd = assetVM.UsrPwd

            };

            return asset;
        }








        [HttpPost]
        public JsonResult save(t_UserInfo order)
        {
            bool status = false;
            //  DateTime dateOrg;
            //  var isValidDate = DateTime.TryParseExact(order.OrderDateString, "mm-dd-yyyy", null, System.Globalization.DateTimeStyles.None, out dateOrg);
            // if (isValidDate)
            //  {
            //      order.OrderDate = dateOrg;
            //   }

            //    var isValidModel = TryUpdateModel(order);
            //// if (isValidModel)
            {
                using (PrjMngEntities dc = new PrjMngEntities())
                {
                    dc.t_UserInfo.Add(order);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }


        [HttpPost]
        public ActionResult update(t_UserInfo emp)
        {
            bool status = false;

            {
                using (PrjMngEntities dc = new PrjMngEntities())
                {
                    if (emp.Eid > 0)
                    {
                        //Edit 
                        var v = dc.t_UserInfo.Where(a => a.Eid == emp.Eid).FirstOrDefault();
                        if (v != null)
                        {
                            v.EmpName = emp.EmpName;
                            v.EmpImg = emp.EmpImg;
                            v.EmailID = emp.EmailID;
                            v.EmpAdrs = emp.EmpAdrs;
                            v.EmpPhone = emp.EmpPhone;
                            v.EmployeeID = emp.EmployeeID;
                            v.UserId = emp.UserId;
                            v.UsrPwd = emp.UsrPwd;
                        }
                    }
                    else
                    {
                        //Save
                        //  dc.Employees.Add(emp);
                    }
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }


        [HttpPost]
        public JsonResult DeleteCustomer(int? Eid)
        {
            using (PrjMngEntities _context = new PrjMngEntities())
            {

                //   int x = Int32.Parse(Eid);
                var emp = _context.t_UserInfo.Find(Eid);
                if (Eid == null)
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                _context.t_UserInfo.Remove(emp);
                _context.SaveChanges();

                return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult Get_AllEmployee()
        {
            using (PrjMngEntities Obj = new PrjMngEntities())
            {
                List<t_UserInfo> Emp = Obj.t_UserInfo.ToList();
                return Json(Emp, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>  
        /// Get Employee With Id  
        /// </summary>  
        /// <param name="Id"></param>  
        /// <returns></returns>  
        public JsonResult Get_EmployeeById(string Id)
        {
            using (PrjMngEntities Obj = new PrjMngEntities())
            {
                int EmpId = int.Parse(Id);
                return Json(Obj.t_UserInfo.Find(EmpId), JsonRequestBehavior.AllowGet);
            }
        }

    }

}
