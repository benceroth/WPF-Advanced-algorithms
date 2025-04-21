﻿namespace Halal.GUI.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Halal.Problems.WorkAssignment;
    using OxyPlot.Axes;
    using OxyPlot.Series;

    /// <inheritdoc/>
    public sealed class WorkAssignmentViewModel : BaseViewModel<Person, Rate>
    {
        private const int FractionalDigits = 4;

        /// <inheritdoc/>
        public override void Setup()
        {
            this.PlotModel.Axes.Add(new CategoryAxis { Position = AxisPosition.Left });
            this.PlotModel.Series.Add(new BarSeries());
            base.Setup();
        }

        /// <inheritdoc/>
        protected override void Plot()
        {
            this.PlotModel.Title = this.GetTitle();
            var series = (BarSeries)this.PlotModel.Series.First();
            series.Items.Clear();
            series.Items.AddRange(this.GetSolutionDataPoints());
        }

        private string GetTitle() => $"{this.Algorithm.Name}{Environment.NewLine}Salary: {this.GetRoundedSalary()} Quality: {this.GetRoundedQuality()}";

        private double GetRoundedSalary() => Math.Round(((Solution)this.Algorithm.Solution).CalculateSalary(), FractionalDigits);

        private double GetRoundedQuality() => Math.Round(((Solution)this.Algorithm.Solution).CalculateQuality(), FractionalDigits);

        private IEnumerable<BarItem> GetSolutionDataPoints() => this.Algorithm.Solution.Select(element => new BarItem(element.Value));
    }
}
