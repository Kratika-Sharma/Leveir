using Leveir.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Leveir.Controllers
{
    [CustomAuthorize]
    public class AdminController : Controller
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        public ActionResult Dashboard()
        {
            return View();
        }

        //Add JewelryTypes Code START
        [HttpGet]
        public ActionResult AddJewelryTypes(int? id)
        {
            JewelryTypes jewelryTypes = new JewelryTypes();
            try
            {
                if (id != null && id > 0)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "SELECT JewelryTypeId, JewelryName, Description, JewelryImg FROM tbl_JewelryType WHERE JewelryTypeId = @JewelryTypeId";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            conn.Open();
                            cmd.Parameters.AddWithValue("@JewelryTypeId", id);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    jewelryTypes.JewelryTypeId = Convert.ToInt32(reader["JewelryTypeId"]);
                                    jewelryTypes.JewelryName = Convert.ToString(reader["JewelryName"]);
                                    jewelryTypes.Description = Convert.ToString(reader["Description"]);
                                    jewelryTypes.JewelryImgName = Convert.ToString(reader["JewelryImg"]);
                                }
                            }
                        }
                    }
                }

                return View(jewelryTypes);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error: " + ex.Message;
                return View(jewelryTypes);
            }
        }

        [HttpPost]
        public ActionResult AddJewelryTypes(JewelryTypes jewelryTypes)
        {
            try
            {
                string fileName = jewelryTypes.JewelryImgName;

                if (jewelryTypes.JewelryImg != null)
                {
                    fileName = Path.GetFileName(jewelryTypes.JewelryImg.FileName);
                    string filePath = Path.Combine(Server.MapPath("/Content/Admin_assets/images/UploadedImages/JewelryType/"), fileName);
                    jewelryTypes.JewelryImg.SaveAs(filePath);
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "";

                    if (jewelryTypes.JewelryTypeId > 0)
                    {
                        query = "UPDATE tbl_JewelryType SET JewelryName = @JewelryName, Description = @Description, JewelryImg = @JewelryImg WHERE JewelryTypeId = @JewelryTypeId";
                    }
                    else
                    {
                        query = "INSERT INTO tbl_JewelryType(JewelryName, Description, JewelryImg, Created_dt) VALUES(@JewelryName, @Description, @JewelryImg, @Created_dt)";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@JewelryName", jewelryTypes.JewelryName);
                        cmd.Parameters.AddWithValue("@Description", jewelryTypes.Description);
                        cmd.Parameters.AddWithValue("@JewelryImg", fileName ?? (object)DBNull.Value);

                        if (jewelryTypes.JewelryTypeId > 0)
                        {
                            cmd.Parameters.AddWithValue("@JewelryTypeId", jewelryTypes.JewelryTypeId);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Created_dt", DateTime.Now);
                        }

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        TempData["JewelryTypeSuccessMsg"] = jewelryTypes.JewelryTypeId > 0 ? "Jewelry Type Updated Successfully" : "Jewelry Type Added Successfully";
                        return RedirectToAction("JewelryTypeLists", "Admin");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error: " + ex.Message;
            }

            return View(jewelryTypes);
        }

        //Jewelry Type Lists Code Start Here
        public ActionResult JewelryTypeLists()
        {
            List<JewelryTypes> jewelrylists = new List<JewelryTypes>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM tbl_JewelryType";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                jewelrylists.Add(new JewelryTypes()
                                {
                                    JewelryTypeId = Convert.ToInt32(reader["JewelryTypeId"]),
                                    JewelryName = Convert.ToString(reader["JewelryName"]),
                                    Description = Convert.ToString(reader["Description"]),
                                    JewelryImgName = Convert.ToString(reader["JewelryImg"]),
                                    Created_dt = Convert.ToDateTime(reader["Created_dt"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                ViewBag.ErrorMessage = "Error" + Ex.Message;
            }
            return View(jewelrylists);
        }

        //Jewelry Type Lists Code End Here

        //Add Setting Style Code Start Here
        [HttpGet]
        public ActionResult AddSettingStyle(int? id)
        {
            var model = new SettingStyles();

            // Get JewelryTypes for Dropdown
            List<JewelryTypes> jewelryTypes = new List<JewelryTypes>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT JewelryTypeId, JewelryName FROM tbl_JewelryType";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    jewelryTypes.Add(new JewelryTypes
                    {
                        JewelryTypeId = Convert.ToInt32(rdr["JewelryTypeId"]),
                        JewelryName = rdr["JewelryName"].ToString()
                    });
                }
            }

            

            if (id.HasValue)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM tbl_SettingStyles WHERE SettingStyleId = @id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id.Value);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                model.SettingStyleId = Convert.ToInt32(reader["SettingStyleId"]);
                                model.JewelryTypeId = Convert.ToInt32(reader["JewelryTypeId"]);
                                model.StyleName = reader["StyleName"].ToString();
                                model.Description = reader["Description"].ToString();
                                model.StyleImg = reader["StyleImg"].ToString();
                                model.StyleQuantity = Convert.ToInt32(reader["StyleQuantity"]);
                                model.StylePrice = Convert.ToDecimal(reader["StylePrice"]);
                            }
                        }
                    }
                }
            }
            ViewBag.JewelryTypes = new SelectList(jewelryTypes, "JewelryTypeId", "JewelryName", model.JewelryTypeId);
            return View(model);
        }
        [HttpPost]
        public ActionResult AddSettingStyle(SettingStyles model)
        {
            try
            {
                string fileName = model.StyleImg;

                if (model.SettingStyleImg != null && model.SettingStyleImg.ContentLength > 0)
                {
                    fileName = Path.GetFileName(model.SettingStyleImg.FileName);
                    string filePath = Path.Combine(Server.MapPath("/Content/Admin_assets/images/UploadedImages/SettingStyle/"), fileName);
                    model.SettingStyleImg.SaveAs(filePath);
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    if (model.SettingStyleId > 0)
                    {
                        string updateQuery = @"UPDATE tbl_SettingStyles 
                    SET JewelryTypeId = @JewelryTypeId, StyleName = @StyleName, 
                        Description = @Description, StyleImg = @StyleImg,
                        StyleQuantity = @StyleQuantity, StylePrice = @StylePrice 
                    WHERE SettingStyleId = @SettingStyleId";

                        using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@JewelryTypeId", model.JewelryTypeId);
                            cmd.Parameters.AddWithValue("@StyleName", model.StyleName);
                            cmd.Parameters.AddWithValue("@Description", model.Description);
                            cmd.Parameters.AddWithValue("@StyleImg", fileName);
                            cmd.Parameters.AddWithValue("@StyleQuantity", model.StyleQuantity);
                            cmd.Parameters.AddWithValue("@StylePrice", model.StylePrice);
                            cmd.Parameters.AddWithValue("@SettingStyleId", model.SettingStyleId);
                            cmd.ExecuteNonQuery();
                        }

                        TempData["StyleSuccessMsg"] = "Setting Style Updated Successfully!";
                    }
                    else
                    {
                        string insertQuery = @"INSERT INTO tbl_SettingStyles 
                    (JewelryTypeId, StyleName, Description, StyleImg, StyleQuantity, StylePrice, Created_dt) 
                    VALUES (@JewelryTypeId, @StyleName, @Description, @StyleImg, @StyleQuantity, @StylePrice, @Created_dt)";

                        using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@JewelryTypeId", model.JewelryTypeId);
                            cmd.Parameters.AddWithValue("@StyleName", model.StyleName);
                            cmd.Parameters.AddWithValue("@Description", model.Description);
                            cmd.Parameters.AddWithValue("@StyleImg", fileName);
                            cmd.Parameters.AddWithValue("@StyleQuantity", model.StyleQuantity);
                            cmd.Parameters.AddWithValue("@StylePrice", model.StylePrice);
                            cmd.Parameters.AddWithValue("@Created_dt", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        TempData["StyleSuccessMsg"] = "Setting Style Added Successfully!";
                    }
                }

                return RedirectToAction("SettingStyleLists");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: " + ex.Message;
                return View(model);
            }
        }


        //Add Setting Style Code End Here

        //Setting Style Lists Code Start Here

        [HttpGet]
        public ActionResult SettingStyleLists()
        {
            List<SettingStyles> settingStyles = new List<SettingStyles>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT B.*, A.JewelryName FROM tbl_JewelryType AS A INNER JOIN tbl_SettingStyles AS B ON A.JewelryTypeId = B.JewelryTypeId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                settingStyles.Add(new SettingStyles()
                                {
                                    SettingStyleId = Convert.ToInt32(reader["SettingStyleId"]),
                                    JewelryTypeId = Convert.ToInt32(reader["JewelryTypeId"]),
                                    StyleName = Convert.ToString(reader["StyleName"]),
                                    JewelryName = Convert.ToString(reader["JewelryName"]),
                                    Description = Convert.ToString(reader["Description"]),
                                    StyleImg = Convert.ToString(reader["StyleImg"]),
                                    StyleQuantity = Convert.ToInt32(reader["StyleQuantity"]),
                                    StylePrice = Convert.ToDecimal(reader["StylePrice"]),
                                    Created_dt = Convert.ToDateTime(reader["Created_dt"])
                                });
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error" + ex.Message;
            }
            return View(settingStyles);
        }

        //Setting Style Lists Code End Here

        // Add Metal Code Start Here
        [HttpGet]
        public ActionResult AddMetals(int? id)
        {
            var model = new Metals();
            //Get JewelryTypes list in dropdown when add metal
            List<JewelryTypes> jewelryTypes = new List<JewelryTypes>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT JewelryTypeId, JewelryName FROM tbl_JewelryType";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    jewelryTypes.Add(new JewelryTypes
                    {
                        JewelryTypeId = Convert.ToInt32(rdr["JewelryTypeId"]),
                        JewelryName = rdr["JewelryName"].ToString()
                    });
                }
            }
            if (id.HasValue)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM tbl_Metals WHERE MetalId = @id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id.Value);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                model.MetalId = Convert.ToInt32(reader["MetalId"]);
                                model.JewelryTypeId = Convert.ToInt32(reader["JewelryTypeId"]);
                                model.SettingStyleId = Convert.ToInt32(reader["SettingStyleId"]);
                                model.MetalName = reader["MetalName"].ToString();
                                model.MetalPrice = Convert.ToDecimal(reader["MetalPrice"]);
                                model.Description = reader["Description"].ToString();
                                model.MetalQuantity = Convert.ToInt32(reader["MetalQuantity"]);
                                model.MetalCarat = reader["MetalCarat"].ToString();
                            }
                        }
                    }
                }
            }
            ViewBag.JewelryTypes = new SelectList(jewelryTypes, "JewelryTypeId", "JewelryName", model.JewelryTypeId);

            // Bind SettingStyle dropdown based on selected JewelryTypeId
            List<SettingStyles> settingStyles = new List<SettingStyles>();
            if (model.JewelryTypeId > 0)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string settingQuery = "SELECT SettingStyleId, StyleName FROM tbl_SettingStyles WHERE JewelryTypeId = @JewelryTypeId";
                    SqlCommand settingCmd = new SqlCommand(settingQuery, conn);
                    settingCmd.Parameters.AddWithValue("@JewelryTypeId", model.JewelryTypeId);
                    conn.Open();
                    SqlDataReader settingReader = settingCmd.ExecuteReader();
                    while (settingReader.Read())
                    {
                        settingStyles.Add(new SettingStyles
                        {
                            SettingStyleId = Convert.ToInt32(settingReader["SettingStyleId"]),
                            StyleName = settingReader["StyleName"].ToString()
                        });
                    }
                }
            }
            ViewBag.SettingStyles = new SelectList(settingStyles, "SettingStyleId", "StyleName", model.SettingStyleId);
            return View(model);
        }
        [HttpPost]
        public ActionResult AddMetals(Metals metals)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    if(metals.MetalId > 0)
                    {
                        string updateQuery = @"UPDATE tbl_Metals SET JewelryTypeId = @JewelryTypeId, SettingStyleId = @SettingStyleId, MetalName = @MetalName, MetalPrice = @MetalPrice, Description = @Description, MetalQuantity = @MetalQuantity, MetalCarat = @MetalCarat WHERE MetalId = @MetalId";

                        using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@JewelryTypeId", metals.JewelryTypeId);
                            cmd.Parameters.AddWithValue("@SettingStyleId", metals.SettingStyleId);
                            cmd.Parameters.AddWithValue("@MetalName", metals.MetalName);
                            cmd.Parameters.AddWithValue("@MetalPrice", metals.MetalPrice);
                            cmd.Parameters.AddWithValue("@Description", metals.Description);
                            cmd.Parameters.AddWithValue("@MetalQuantity", metals.MetalQuantity);
                            cmd.Parameters.AddWithValue("@MetalCarat", metals.MetalCarat);
                            cmd.Parameters.AddWithValue("@MetalId", metals.MetalId);
                            cmd.ExecuteNonQuery();
                        }

                        TempData["MetalSuccessMsg"] = "Metal Updated Successfully!";
                    }
                    else
                    {
                        string query = "INSERT INTO tbl_Metals(JewelryTypeId, SettingStyleId, MetalName, MetalPrice, Description, MetalQuantity, MetalCarat, Created_dt) VALUES(@JewelryTypeId, @SettingStyleId, @MetalName, @MetalPrice, @Description, @MetalQuantity, @MetalCarat, @Created_dt)";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@JewelryTypeId", metals.JewelryTypeId);
                            cmd.Parameters.AddWithValue("@SettingStyleId", metals.SettingStyleId);
                            cmd.Parameters.AddWithValue("@MetalName", metals.MetalName);
                            cmd.Parameters.AddWithValue("@MetalPrice", metals.MetalPrice);
                            cmd.Parameters.AddWithValue("@Description", metals.Description);
                            cmd.Parameters.AddWithValue("@MetalQuantity", metals.MetalQuantity);
                            cmd.Parameters.AddWithValue("@MetalCarat", metals.MetalCarat);
                            cmd.Parameters.AddWithValue("@Created_dt", DateTime.Now);
                           // con.Open();
                            cmd.ExecuteNonQuery();
                            TempData["MetalSuccessMsg"] = "Metal Added Successfully!";
                            return RedirectToAction("MetalLists");
                        }
                    }
                        
                }
                return RedirectToAction("MetalLists");
            }
            catch (SqlException ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View();
        }
        // Get Style setting data in dropdown when add metal
        [HttpGet]
        public JsonResult GetSettingStyles(int jewelryTypeId)
        {
            List<SettingStyles> styles = new List<SettingStyles>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT SettingStyleId, StyleName FROM tbl_SettingStyles WHERE JewelryTypeId = @JewelryTypeId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@JewelryTypeId", jewelryTypeId);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    styles.Add(new SettingStyles
                    {
                        SettingStyleId = Convert.ToInt32(rdr["SettingStyleId"]),
                        StyleName = rdr["StyleName"].ToString()
                    });
                }
            }

            return Json(styles, JsonRequestBehavior.AllowGet);
        }

        // Add Metal Code End Here
        // Metal Lists Code START
        [HttpGet]
        public ActionResult MetalLists()
        {
            List<Metals> metal = new List<Metals>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = " SELECT m.MetalId, j.JewelryTypeId, j.JewelryName, s.SettingStyleId, s.StyleName, m.MetalName, m.MetalPrice, m.Description, m.MetalQuantity, m.MetalCarat, m.Created_dt FROM tbl_Metals m INNER JOIN tbl_JewelryType j ON m.JewelryTypeId = j.JewelryTypeId INNER JOIN tbl_SettingStyles s ON m.SettingStyleId = s.SettingStyleId AND m.JewelryTypeId = s.JewelryTypeId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                metal.Add(new Metals()
                                {
                                    MetalId = Convert.ToInt32(reader["MetalId"]),
                                    JewelryTypeId = Convert.ToInt32(reader["JewelryTypeId"]),
                                    JewelryName = Convert.ToString(reader["JewelryName"]),
                                    SettingStyleId = Convert.ToInt32(reader["SettingStyleId"]),
                                    StyleName = Convert.ToString(reader["StyleName"]),
                                    MetalName = Convert.ToString(reader["MetalName"]),
                                    MetalPrice = Convert.ToDecimal(reader["MetalPrice"]),
                                    Description = Convert.ToString(reader["Description"]),
                                    MetalQuantity = Convert.ToInt32(reader["MetalQuantity"]),
                                    MetalCarat = Convert.ToString(reader["MetalCarat"]),
                                    Created_dt = Convert.ToDateTime(reader["Created_dt"])
                                });
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error" + ex.Message;
            }
            return View(metal);
        }
        // Metal Lists Code END


        //Add Diamond Type Code Start Here

        [HttpGet]
        public ActionResult AddDiamondType(int? id)
        {
            Diamonds model = new Diamonds();
            try
            {
                if (id.HasValue)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "SELECT DiamondTypeId, DiamondTypeName FROM tbl_DiamondType WHERE DiamondTypeId = @DiamondTypeId";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@DiamondTypeId", id);
                            conn.Open();
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    model.DiamondTypeId = Convert.ToInt32(reader["DiamondTypeId"]);
                                    model.DiamondTypeName = Convert.ToString(reader["DiamondTypeName"]);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = "Error: " + ex.Message;
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult AddDiamondType(Diamonds model)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    if (model.DiamondTypeId > 0)
                    {
                        string query = "UPDATE tbl_DiamondType SET DiamondTypeName = @DiamondTypeName WHERE DiamondTypeId = @DiamondTypeId";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@DiamondTypeId", model.DiamondTypeId);
                            cmd.Parameters.AddWithValue("@DiamondTypeName", model.DiamondTypeName);
                            conn.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                TempData["DiamondTypeSuccMsg"] = "Diamond Type Updated Successfully";
                                return RedirectToAction("DiamondTypeLists", "Admin");
                            }
                        }
                    }
                    else
                    {
                        string query = "INSERT INTO tbl_DiamondType(DiamondTypeName) VALUES(@DiamondTypeName)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@DiamondTypeName", model.DiamondTypeName);
                            conn.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                TempData["DiamondTypeSuccMsg"] = "Diamond Type Recorded Successfully";
                                return RedirectToAction("DiamondTypeLists", "Admin");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
            }

            return View(model);
        }

        //Add Diamond Type Code End Here

        //Diamond Type Lists Code Start Here
        public ActionResult DiamondTypeLists()
        {
            List<Diamonds> diamondslists = new List<Diamonds>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM tbl_DiamondType";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                diamondslists.Add(new Diamonds()
                                {
                                    DiamondTypeId = Convert.ToInt32(reader["DiamondTypeId"]),
                                    DiamondTypeName = Convert.ToString(reader["DiamondTypeName"]),
                                    Created_dt = Convert.ToDateTime(reader["Created_dt"])
                                });
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                ViewBag.Error = "Error" + ex.Message;
            }
            return View(diamondslists);
        }
        //Diamond Type Lists Code End Here

        //Add Diamond Code Start Here

        [HttpGet]
        public ActionResult AddDiamonds(int? id)
        {
            Diamonds model = new Diamonds();
            List<Diamonds> diamondtypes = new List<Diamonds>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM tbl_DiamondType";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                diamondtypes.Add(new Diamonds()
                                {
                                    DiamondTypeId = Convert.ToInt32(reader["DiamondTypeId"]),
                                    DiamondTypeName = Convert.ToString(reader["DiamondTypeName"]),
                                });
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                ViewBag.Error = "Error" + ex.Message;
            }
            ViewBag.DiamondLists = diamondtypes;

            if (!id.HasValue)
            {
                ViewBag.DiamondLists = diamondtypes;
                return View(model);
            }
            else
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "SELECT * FROM tbl_Diamond WHERE DiamondId = @id";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            conn.Open();
                            cmd.Parameters.AddWithValue("@id", id.Value);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    model.DiamondId = Convert.ToInt32(reader["DiamondId"]);
                                    model.DiamondTypeId = Convert.ToInt32(reader["DiamondTypeId"]);
                                    model.Shape = Convert.ToString(reader["Shape"]);
                                    model.Color = Convert.ToString(reader["Color"]);
                                    model.Clarity = Convert.ToString(reader["Clarity"]);
                                    model.Carat = Convert.ToString(reader["Carat"]);
                                    model.Cut = Convert.ToString(reader["Cut"]);
                                    model.Price = Convert.ToDecimal(reader["Price"]);
                                    model.StockQuantity = Convert.ToInt32(reader["StockQuantity"]);
                                    model.DiamondImgName = Convert.ToString(reader["DiamondImg"]);
                                }
                            }
                        }
                    }
                }

                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Error" + ex.Message;
                }


            }

            return View(model);
        }

        [HttpPost]
        public ActionResult AddDiamonds(Diamonds model)
        {
            try
            {
                string fileName = model.DiamondImgName;

                if (model.DiamondImg != null && model.DiamondImg.ContentLength > 0)
                {
                    fileName = Path.GetFileName(model.DiamondImg.FileName);
                    string filePath = Path.Combine(Server.MapPath("/Content/Admin_assets/images/UploadedImages/DiamondImages/"), fileName);
                    model.DiamondImg.SaveAs(filePath);
                }

                if (model.DiamondId > 0)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "UPDATE tbl_Diamond SET DiamondTypeId = @DiamondTypeId, Shape = @Shape, Color = @Color, Clarity = @Clarity, Carat = @Carat, Cut = @Cut, Price = @Price, StockQuantity = @StockQuantity, DiamondImg = @DiamondImg WHERE DiamondId = @DiamondId";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@DiamondId", model.DiamondId);
                            cmd.Parameters.AddWithValue("@DiamondTypeId", model.DiamondTypeId);
                            cmd.Parameters.AddWithValue("@Shape", model.Shape);
                            cmd.Parameters.AddWithValue("@Color", model.Color);
                            cmd.Parameters.AddWithValue("@Clarity", model.Clarity);
                            cmd.Parameters.AddWithValue("@Carat", model.Carat);
                            cmd.Parameters.AddWithValue("@Cut", model.Cut);
                            cmd.Parameters.AddWithValue("@Price", model.Price);
                            cmd.Parameters.AddWithValue("@StockQuantity", model.StockQuantity);
                            cmd.Parameters.AddWithValue("@DiamondImg", fileName);

                            conn.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                TempData["DiamondAddSuccMsg"] = "Diamond Updated Successfully";
                                return RedirectToAction("DiamondLists", "Admin");
                            }
                            else
                            {
                                return View();
                            }
                        }
                    }
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "INSERT INTO tbl_Diamond (DiamondTypeId, Shape, Color, Clarity, Carat, Cut, Price, StockQuantity, DiamondImg)" +
                            "VALUES(@DiamondTypeId, @Shape, @Color, @Clarity, @Carat, @Cut, @Price, @StockQuantity, @DiamondImg)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@DiamondId", model.DiamondId);
                            cmd.Parameters.AddWithValue("@DiamondTypeId", model.DiamondTypeId);
                            cmd.Parameters.AddWithValue("@Shape", model.Shape);
                            cmd.Parameters.AddWithValue("@Color", model.Color);
                            cmd.Parameters.AddWithValue("@Clarity", model.Clarity);
                            cmd.Parameters.AddWithValue("@Carat", model.Carat);
                            cmd.Parameters.AddWithValue("@Cut", model.Cut);
                            cmd.Parameters.AddWithValue("@Price", model.Price);
                            cmd.Parameters.AddWithValue("@StockQuantity", model.StockQuantity);
                            cmd.Parameters.AddWithValue("@DiamondImg", fileName);

                            conn.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                TempData["DiamondAddSuccMsg"] = "Diamond Recorded Successfully";
                                return RedirectToAction("DiamondLists", "Admin");
                            }
                            else
                            {
                                return View();
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                ViewBag.ErrorMessage = Ex.Message;
            }
            return View();
        }

        //Add Diamond Code End Here

        //Diamond Lists Code Start Here

        [HttpGet]
        public ActionResult DiamondLists()
        {
            List<Diamonds> diamondLists = new List<Diamonds>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT B.*, A.DiamondTypeName FROM tbl_DiamondType AS A INNER JOIN tbl_Diamond AS B ON A.DiamondTypeId = B.DiamondTypeId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                diamondLists.Add(new Diamonds()
                                {
                                    DiamondId = Convert.ToInt32(reader["DiamondId"]),
                                    DiamondTypeId = Convert.ToInt32(reader["DiamondTypeId"]),
                                    DiamondTypeName = Convert.ToString(reader["DiamondTypeName"]),
                                    Shape = Convert.ToString(reader["Shape"]),
                                    Color = Convert.ToString(reader["Color"]),
                                    Clarity = Convert.ToString(reader["Clarity"]),
                                    Carat = Convert.ToString(reader["Carat"]),
                                    Cut = Convert.ToString(reader["Cut"]),
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    StockQuantity = Convert.ToInt32(reader["StockQuantity"]),
                                    DiamondImgName = Convert.ToString(reader["DiamondImg"]),
                                    Created_dt = Convert.ToDateTime(reader["Created_dt"])
                                });
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error" + ex.Message;
            }
            return View(diamondLists);
        }

        //Diamond Lists Code End Here



    }
}