using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.DesktopEdition;

namespace  EarthQuakesHeatMap
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.CurrentExtent = new RectangleShape(-125,47,-67,25);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 198, 255, 255));

            //Displays the World Map Kit as a background.
            WorldMapKitWmsDesktopOverlay worldMapKitDesktopOverlay = new WorldMapKitWmsDesktopOverlay();
            winformsMap1.Overlays.Add(worldMapKitDesktopOverlay);

            //Point based ShapeFileFeatureSource on earth quakes used for the HeatOverlay.
            ShapeFileFeatureSource featureSource = new ShapeFileFeatureSource(@"..\..\Data\quksigx020.shp"); 
            //Creates the HeatOverlay with the point based ShapeFileFeatureSource for earthquakes.
            HeatLayer heatLayer = new HeatLayer(featureSource);
            //Creates the HeatStyle to set the properties determining the display of the heat map with earth quake data.
            //Notice that each point is treated with an intensity depending on the value of the column "other_magn1".
            //So, in addition to the density of points location, the value for each point is also going to be counted into account
            //for the coloring of the map.
            HeatStyle heatStyle = new HeatStyle();
            heatStyle.IntensityColumnName = "other_mag1";
            heatStyle.IntensityRangeStart = 0;
            heatStyle.IntensityRangeEnd = 12;
            //Sets the size of each point 100 kilometers of diameter.
            heatStyle.Alpha = 180;
            heatStyle.PointRadius = 60; 
            heatStyle.PointRadiusUnit = DistanceUnit.Kilometer;

            heatLayer.HeatStyle = heatStyle;

            LayerOverlay heatMapOverlay = new LayerOverlay();
            heatMapOverlay.Layers.Add(heatLayer);

            winformsMap1.Overlays.Add("HeatOverlay", heatMapOverlay);

            winformsMap1.Refresh();
        }

        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    
    }
}
