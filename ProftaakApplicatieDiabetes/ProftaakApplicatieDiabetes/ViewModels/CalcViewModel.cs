﻿using Models;
using System.ComponentModel.DataAnnotations;

namespace ProftaakApplicatieDiabetes.Models
{
    public class CalcViewModel
    {
        public int Id { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Your current weight must be numeral and can't be 0.")]
        [Display(Name = "Current weight")]
        [Required(ErrorMessage = "Your current weight is required to calculate the insuline amount.")]
        public int Weight { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Your total carbs income must be numeral and can't be 0.")]
        [Display(Name = "Total carbs income")]
        [Required(ErrorMessage = "The total carbs income is required to calculate the insuline amount")]
        public int TotalCarbs { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Your current blood sugar must be numeral and can't be 0.")]
        [Display(Name = "Current blood sugar")]
        [Required(ErrorMessage = "Your current blood sugar is required to calculate te insuline amount.")]
        public int CurrentBloodsugar { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Your targeted blood sugar must be numeral and can't be 0.")]
        [Display(Name = "Target blood sugar")]
        [Required(ErrorMessage = "The targeted blood sugar is required to calculate the insuline amount.")]
        public int TargetBloodSugar { get; set; }

        public int Result { get; set; }

        public Calculation CalculatorResults;
    }
}
