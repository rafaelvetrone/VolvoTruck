using System;
using Xunit;
using TruckPortal.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using TruckPortal.Models;
using System.Collections.Generic;
using TruckPortal.DAL;
using TruckPortal.Entities;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        public UnitTest1()
        {
            //Database setup
            var sqlCon = new SqlConnection(@"Server=BRCTAW10351481\SQL2017;Database=TruckPortal;Integrated Security=True");

            sqlCon.Open();

            string command = "DELETE Trucks";

            SqlCommand com = new SqlCommand(command, sqlCon);
            com.ExecuteNonQuery();

            command = "DBCC CHECKIDENT ('Trucks', RESEED, 0);";

            com = new SqlCommand(command, sqlCon);
            com.ExecuteNonQuery();

            command = "INSERT INTO Trucks (ModelId,ModelName,DeliveryYear,ModelYear) VALUES (1, 'FH', 2019, 2019)";
            com = new SqlCommand(command, sqlCon);
            com.ExecuteNonQuery();

            command = "INSERT INTO Trucks (ModelId,ModelName,DeliveryYear,ModelYear) VALUES (1, 'FH', 2019, 2020)";
            com = new SqlCommand(command, sqlCon);
            com.ExecuteNonQuery();

            command = "INSERT INTO Trucks (ModelId,ModelName,DeliveryYear,ModelYear) VALUES (2, 'FH', 2019, 2019)";
            com = new SqlCommand(command, sqlCon);
            com.ExecuteNonQuery();

            command = "INSERT INTO Trucks (ModelId,ModelName,DeliveryYear,ModelYear) VALUES (2, 'FH', 2019, 2020)";
            com = new SqlCommand(command, sqlCon);
            com.ExecuteNonQuery();            
        }

        [Fact]
        public void TestIndex()
        {
            HomeController controller = new HomeController();

            var result = controller.Index() as ViewResult;
            Assert.IsType<ViewResult>(result);

            Assert.Equal("Index", result.ViewName);

            var model = result.Model as List<TruckViewModel>;

            Assert.Equal(4, model.Count);
        }

        [Fact]
        public  void TestCreate()
        {
            HomeController controller = new HomeController();

            var result = controller.Create() as ViewResult;
            Assert.IsType<ViewResult>(result);

            Assert.Equal("Create", result.ViewName);

            TruckViewModel truckVm = new TruckViewModel();

            truckVm.ModelId = 1;
            truckVm.DeliveryYear = 2019;
            truckVm.ModelYear = 2020;

            result = controller.Create(truckVm) as ViewResult;

            TruckContext db = new TruckContext();

            Truck truck = db.Trucks.Find(5);

            Assert.Equal("FH", truck.ModelName);
        }

        [Fact]
        public void TestCreateError()
        {
            HomeController controller = new HomeController();

            var result = controller.Create() as ViewResult;
            Assert.IsType<ViewResult>(result);

            Assert.Equal("Create", result.ViewName);

            TruckViewModel truckVm = new TruckViewModel();

            truckVm.ModelId = 1;
            truckVm.DeliveryYear = 2019;
            truckVm.ModelYear = 2022;

            result = controller.Create(truckVm) as ViewResult;            
            
            Assert.True(controller.ModelState.Count == 1, "Model year must be current year or next year");
        }

        [Fact]
        public void TestEdit()
        {
            HomeController controller = new HomeController();

            var result = controller.Edit(2) as ViewResult;
            Assert.IsType<ViewResult>(result);
                       
            var model = result.Model as TruckViewModel;

            Assert.Equal(2020, model.ModelYear);
        }

        [Fact]
        public void TestEditPost()
        {
            HomeController controller = new HomeController();

            var result = controller.Edit(2) as ViewResult;
            Assert.IsType<ViewResult>(result);

            var model = result.Model as TruckViewModel;
                       
            Assert.Equal(2020, model.ModelYear);

            model.ModelYear = 2019;

            result = controller.Edit(2, model) as ViewResult;

            TruckContext db = new TruckContext();

            Truck truck = db.Trucks.Find(2);

            Assert.Equal(2019, truck.ModelYear);
        }

        [Fact]
        public void TestDelete()
        {
            HomeController controller = new HomeController();

            var result = controller.Delete(2) as ViewResult;
            Assert.IsType<ViewResult>(result);

            var model = result.Model as TruckViewModel;

            Assert.Equal(2020, model.ModelYear);
        }

        [Fact]
        public void TestDeletePost()
        {
            HomeController controller = new HomeController();

            var result = controller.Delete(2) as ViewResult;
            Assert.IsType<ViewResult>(result);

            var model = result.Model as TruckViewModel;

            Assert.Equal(2020, model.ModelYear);

            model.ModelYear = 2019;

            result = controller.Delete(2, model) as ViewResult;

            TruckContext db = new TruckContext();

            Truck truck = db.Trucks.Find(2);

            Assert.Null(truck);
        }


    }
}
