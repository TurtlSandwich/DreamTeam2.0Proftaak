﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProftaakApplicatieDiabetes.Models;

namespace ProftaakApplicatieDiabetes.Controllers
{
    public class CalcController : Controller
    {
        private readonly ICalculationLogic calcLogic;
        private int hardcodedBSN = 226044440;

        public CalcController(ICalculationLogic _calcLogic)
        {
            calcLogic = _calcLogic;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Calculate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Calculate(CalcModel model)
        {
            model.userBSN = hardcodedBSN;
            ViewBag.Result = Math.Round(calcLogic.CalculateMealtimeDose(new Calculation(model.userBSN, model.Weight, model.TotalCarbs, model.CurrentBloodsugar, model.TargetBloodSugar)));

            return View();
        }
    }
}