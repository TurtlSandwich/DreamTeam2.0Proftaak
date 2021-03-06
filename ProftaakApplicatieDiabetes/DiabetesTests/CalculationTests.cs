﻿using Data.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using Data.Contexts;
using Models;
using Logic;

namespace DiabetesTests
{
    [TestClass]
    public class CalculationTests
    {
        [TestMethod]
        public void CalculateMealtimeDose_Returns7_WhenWeightIs70()
        {
            //Arrange
            ICalculation calculation = new Calculation
            {
                Weight = 70,
                CurrentBloodsugar = 220,
                TargetBloodSugar = 120,
                TotalCarbs = 60
            };
            var memory = new CalculationContextMemory();

            //Act
            var output = memory.CalculateMealtimeDose(calculation);

            //Assert
            Assert.AreEqual(output, 7);
        }

        [TestMethod]
        public void CalculateTDI_Returns38_WhenWeightIs70()
        {
            //Arrange
            var weight = 70;
            var memory = new CalculationClass();

            //Act
            var output = memory.CalculateTotalDoseInsuline(weight);

            //Assert
            Assert.AreEqual(output, 38.5);
        }

        [TestMethod]
        public void CalculateCorrectionFactor_Returns47_WhenWeightIs70()
        {
            //Arrange
            var weight = 70;
            var memory = new CalculationClass();

            //Act
            var output = Math.Round(memory.CalculateCorrectionFactor(weight));

            //Assert
            Assert.AreEqual(output, 47);
        }

        [TestMethod]
        public void CalculateSugarCorrection_Returns47_WhenWeightIs70()
        {
            //Arrange
            var weight = 70;
            var currentBS = 220;
            var targetBS = 120;
            var memory = new CalculationClass();

            //Act
            var output = Math.Round(memory.CalculateSugarCorrection(currentBS, targetBS, weight));

            //Assert
            Assert.AreEqual(output, 2);
        }
        
            [TestMethod]
        public void CalculateCHO_Returns5_WhenWeightIs70()
        {
            //Arrange
            var weight = 70;
            var carbs = 60;
            var memory = new CalculationClass();

            //Act
            var output = Math.Round(memory.CalculateCHO(carbs, weight));

            //Assert
            Assert.AreEqual(output, 5);
        }

        [TestMethod]
        public void CalculateMealtimeDose_TestCase_01()
        {
            //Arrange
            ICalculation calculation = new Calculation
            {
                Weight = 70,
                CurrentBloodsugar = 130,
                TargetBloodSugar = 100,
                TotalCarbs = 60
            };
            var weight = 70;
            var currentBS = 130;
            var targetBS = 100;
            var carbs = 60;
            var memory = new CalculationContextMemory();
            var memory2 = new CalculationClass();

            //Act
            var TMD = memory.CalculateMealtimeDose(calculation);
            var TDI = memory2.CalculateTotalDoseInsuline(weight);
            var CHO = Math.Round(memory2.CalculateCHO(carbs, weight), 2);
            var SC = Math.Round(memory2.CalculateSugarCorrection(currentBS, targetBS, weight), 2);

            //Assert
            Assert.AreEqual(38.5, TDI);
            Assert.AreEqual(5, TMD);
            Assert.AreEqual(4.62, CHO);
            Assert.AreEqual(0.64, SC);
        }

        [TestMethod]
        public void GetSpecificAdvice_Tests()
        {
            Mock<Calculation> mockCalc = new Mock<Calculation>();
            Mock<ICalculationContext> mockContext = new Mock<ICalculationContext>();
            CalculationLogic _logic = new CalculationLogic(mockContext.Object);


            mockContext.Setup(x => x.GetSpecificAdvice(1))
                .Returns(mockCalc.Object);

            var result = _logic.GetSpecificAdvice(1);

            Assert.AreEqual(mockCalc.Object, result);
            mockContext.Verify(x => x.GetSpecificAdvice(1), Times.Once);
        }

    }
}
