using ChartJSCore.Helpers;
using ChartJSCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid_19_GalileiGalileo.Tool
{
        public enum ChartPalette { blue, pink, violette, red, orange, yellow }

    public class ChartData
    {
        public string DatasetName = "Chart Tool";
        public IList<double?> Data = new List<double?>();
        public ChartPalette ChartPalette = ChartPalette.blue;
    }



    public class ChartTool
    {

        private static string _Fill = "true";
        private static float _LineTension = 0.1f;
        private static ChartColor _BackgroundColor = ChartColor.FromRgba(75, 192, 192, 0.4);
        private static ChartColor _BorderColor = ChartColor.FromRgb(75, 192, 192);
        private static string _BorderCapStyle = "butt";
        private static IList<int> _BorderDash = new List<int> { };
        private static float _BorderDashOffset = 0.0f;
        private static string _BorderJoinStyle = "miter";
        private static IList<ChartColor> _PointBorderColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) };
        private static IList<ChartColor> _PointBackgroundColor = new List<ChartColor> { ChartColor.FromHexString("#ffffff") };
        private static List<int> _PointBorderWidth = new List<int> { 1 };
        private static List<int> _PointHoverRadius = new List<int> { 5 };
        private static List<ChartColor> _PointHoverBackgroundColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) };
        private static List<ChartColor> _PointHoverBorderColor = new List<ChartColor> { ChartColor.FromRgb(220, 220, 220) };
        private static List<int> _PointHoverBorderWidth = new List<int> { 2 };
        private static List<int> _PointRadius = new List<int> { 1 };
        private static List<int> _PointHitRadius = new List<int> { 10 };
        private static bool _SpanGaps = false;



        public static Chart CreateChart(IList<string> LabelList, IList<ChartData> chartDatas)
        {

            Chart chart = new Chart()
            {
                Type = Enums.ChartType.Line,
                Options = new Options()
                {
                    Legend = new Legend()
                    {
                        Labels = new LegendLabel()
                        {
                            FontFamily = "Trebuchet MS"
                        }
                    }

                }
            };

            Data data = new Data()
            {
                Labels = LabelList,
                Datasets = new List<Dataset>()
            };

            for (int i = 0; i < chartDatas.Count; i++)
            {
                LineDataset LineDataset = SetColorPalette(chartDatas[i].ChartPalette);
                LineDataset.Data = chartDatas[i].Data.Reverse().ToList();
                LineDataset.Label = chartDatas[i].DatasetName;

                data.Datasets.Add(LineDataset);
            }
            data.Labels = data.Labels.Reverse().ToList();

            chart.Data = data;


            return chart;
        }



        private static LineDataset SetColorPalette(ChartPalette chartPalette)
        {
            switch (chartPalette)
            {
                case ChartPalette.blue:
                    return new LineDataset()
                    {
                        Fill = "true",
                        LineTension = 0.1f,
                        BackgroundColor = ChartColor.FromRgba(75, 192, 192, 0.4),
                        BorderColor = ChartColor.FromRgb(75, 192, 192),
                        BorderCapStyle = "butt",
                        BorderDash = new List<int> { },
                        BorderDashOffset = 0.0f,
                        BorderJoinStyle = "miter",
                        PointBorderColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
                        PointBackgroundColor = new List<ChartColor> { ChartColor.FromHexString("#ffffff") },
                        PointBorderWidth = new List<int> { 1 },
                        PointHoverRadius = new List<int> { 5 },
                        PointHoverBackgroundColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
                        PointHoverBorderColor = new List<ChartColor> { ChartColor.FromRgb(220, 220, 220) },
                        PointHoverBorderWidth = new List<int> { 2 },
                        PointRadius = new List<int> { 1 },
                        PointHitRadius = new List<int> { 10 },
                        SpanGaps = false
                    };

                    
                case ChartPalette.orange:
                    return new LineDataset()
                    {
                        Fill = "true",
                        LineTension = 0.1f,
                        BackgroundColor = ChartColor.FromRgba(250, 200, 140, 0.8),
                        BorderColor = ChartColor.FromRgb(201, 133, 50),
                        BorderCapStyle = "butt",
                        BorderDash = new List<int> { },
                        BorderDashOffset = 0.0f,
                        BorderJoinStyle = "miter",
                        PointBorderColor = new List<ChartColor> { ChartColor.FromRgb(250, 151, 32) },
                        PointBackgroundColor = new List<ChartColor> { ChartColor.FromHexString("#ffffff") },
                        PointBorderWidth = new List<int> { 1 },
                        PointHoverRadius = new List<int> { 5 },
                        PointHoverBackgroundColor = new List<ChartColor> { ChartColor.FromRgb(191, 139, 77) },
                        PointHoverBorderColor = new List<ChartColor> { ChartColor.FromRgb(194, 112, 14) },
                        PointHoverBorderWidth = new List<int> { 2 },
                        PointRadius = new List<int> { 1 },
                        PointHitRadius = new List<int> { 10 },
                        SpanGaps = false
                    };
                case ChartPalette.yellow:
                    return new LineDataset()
                    {
                        Fill = "true",
                        LineTension = 0.1f,
                        BackgroundColor = ChartColor.FromRgba(75, 192, 192, 0.4),
                        BorderColor = ChartColor.FromRgb(75, 192, 192),
                        BorderCapStyle = "butt",
                        BorderDash = new List<int> { },
                        BorderDashOffset = 0.0f,
                        BorderJoinStyle = "miter",
                        PointBorderColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
                        PointBackgroundColor = new List<ChartColor> { ChartColor.FromHexString("#ffffff") },
                        PointBorderWidth = new List<int> { 1 },
                        PointHoverRadius = new List<int> { 5 },
                        PointHoverBackgroundColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
                        PointHoverBorderColor = new List<ChartColor> { ChartColor.FromRgb(220, 220, 220) },
                        PointHoverBorderWidth = new List<int> { 2 },
                        PointRadius = new List<int> { 1 },
                        PointHitRadius = new List<int> { 10 },
                        SpanGaps = false
                    };
                case ChartPalette.red:
                    return new LineDataset()
                    {
                        Fill = "true",
                        LineTension = 0.1f,
                        BackgroundColor = ChartColor.FromRgba(75, 192, 192, 0.4),
                        BorderColor = ChartColor.FromRgb(75, 192, 192),
                        BorderCapStyle = "butt",
                        BorderDash = new List<int> { },
                        BorderDashOffset = 0.0f,
                        BorderJoinStyle = "miter",
                        PointBorderColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
                        PointBackgroundColor = new List<ChartColor> { ChartColor.FromHexString("#ffffff") },
                        PointBorderWidth = new List<int> { 1 },
                        PointHoverRadius = new List<int> { 5 },
                        PointHoverBackgroundColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
                        PointHoverBorderColor = new List<ChartColor> { ChartColor.FromRgb(220, 220, 220) },
                        PointHoverBorderWidth = new List<int> { 2 },
                        PointRadius = new List<int> { 1 },
                        PointHitRadius = new List<int> { 10 },
                        SpanGaps = false
                    };
                case ChartPalette.violette:
                    return new LineDataset()
                    {
                        Fill = "true",
                        LineTension = 0.1f,
                        BackgroundColor = ChartColor.FromRgba(75, 192, 192, 0.4),
                        BorderColor = ChartColor.FromRgb(75, 192, 192),
                        BorderCapStyle = "butt",
                        BorderDash = new List<int> { },
                        BorderDashOffset = 0.0f,
                        BorderJoinStyle = "miter",
                        PointBorderColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
                        PointBackgroundColor = new List<ChartColor> { ChartColor.FromHexString("#ffffff") },
                        PointBorderWidth = new List<int> { 1 },
                        PointHoverRadius = new List<int> { 5 },
                        PointHoverBackgroundColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
                        PointHoverBorderColor = new List<ChartColor> { ChartColor.FromRgb(220, 220, 220) },
                        PointHoverBorderWidth = new List<int> { 2 },
                        PointRadius = new List<int> { 1 },
                        PointHitRadius = new List<int> { 10 },
                        SpanGaps = false
                    };
                case ChartPalette.pink:
                    return new LineDataset()
                    {
                        Fill = "true",
                        LineTension = 0.1f,
                        BackgroundColor = ChartColor.FromRgba(75, 192, 192, 0.4),
                        BorderColor = ChartColor.FromRgb(75, 192, 192),
                        BorderCapStyle = "butt",
                        BorderDash = new List<int> { },
                        BorderDashOffset = 0.0f,
                        BorderJoinStyle = "miter",
                        PointBorderColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
                        PointBackgroundColor = new List<ChartColor> { ChartColor.FromHexString("#ffffff") },
                        PointBorderWidth = new List<int> { 1 },
                        PointHoverRadius = new List<int> { 5 },
                        PointHoverBackgroundColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
                        PointHoverBorderColor = new List<ChartColor> { ChartColor.FromRgb(220, 220, 220) },
                        PointHoverBorderWidth = new List<int> { 2 },
                        PointRadius = new List<int> { 1 },
                        PointHitRadius = new List<int> { 10 },
                        SpanGaps = false
                    };


                default:
                    return new LineDataset()
                    {
                        Fill = "true",
                        LineTension = 0.1f,
                        BackgroundColor = ChartColor.FromRgba(75, 192, 192, 0.4),
                        BorderColor = ChartColor.FromRgb(75, 192, 192),
                        BorderCapStyle = "butt",
                        BorderDash = new List<int> { },
                        BorderDashOffset = 0.0f,
                        BorderJoinStyle = "miter",
                        PointBorderColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
                        PointBackgroundColor = new List<ChartColor> { ChartColor.FromHexString("#ffffff") },
                        PointBorderWidth = new List<int> { 1 },
                        PointHoverRadius = new List<int> { 5 },
                        PointHoverBackgroundColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
                        PointHoverBorderColor = new List<ChartColor> { ChartColor.FromRgb(220, 220, 220) },
                        PointHoverBorderWidth = new List<int> { 2 },
                        PointRadius = new List<int> { 1 },
                        PointHitRadius = new List<int> { 10 },
                        SpanGaps = false
                    };
            }
        }


    }
}
