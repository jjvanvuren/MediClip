using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//==============================================================================================
//Reference D1: Video Tutorial
//Purpose: Used to learn how to create a web API service and connect to a SQL database.
//Date: 19/10/2018
//Source: YouTube
//Author: Tony Seo
//URL: https://www.youtube.com/watch?v=ddXVMdeA5D0
//Adaption Required: Information learned from the video was used to create this web API service.
//==============================================================================================

namespace MediClipWebAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
