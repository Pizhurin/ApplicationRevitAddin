using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;

namespace PC_App.ViewModel
{
    public class ViewModel_CountElements : INotifyPropertyChanged
    {
        private ExternalCommandData _commandData;

        public DelegateCommand QuantityPipes { get; }
        public DelegateCommand VolumeWalls { get; }
        public DelegateCommand QuantityDoors { get; }

        private string _resultCommand = String.Empty;
        public string ResultCommand
        {
            get { return _resultCommand; }
            set { _resultCommand = value; NotifyPropertyChanged(nameof(ResultCommand)); }
        }


        public ViewModel_CountElements(ExternalCommandData commandData)
        {
            _commandData = commandData;
            QuantityPipes = new DelegateCommand(GetQuantityPipes);
            VolumeWalls = new DelegateCommand(GetVolumeWalls);
            QuantityDoors = new DelegateCommand(GetQuantityDoors);
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void GetQuantityDoors()
        {
            Document document = _commandData.Application.ActiveUIDocument.Document;

            var doors = new FilteredElementCollector(document)
                .WhereElementIsNotElementType()
                .OfClass(typeof(FamilyInstance))
                .OfCategory(BuiltInCategory.OST_Doors)
                .ToList();

            ResultCommand = $"Количество дверей {doors.Count} шт.";
        }

        private void GetVolumeWalls()
        {
            Document document = _commandData.Application.ActiveUIDocument.Document;

            var walls = new FilteredElementCollector(document)
                .WhereElementIsNotElementType()
                .OfCategory(BuiltInCategory.OST_Walls)
                .Cast<Wall>()
                .ToList();
            double result = 0.00;
            foreach (Wall w in walls)
            {
                result += UnitUtils.ConvertFromInternalUnits(w.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED).AsDouble(), UnitTypeId.CubicMeters);
            }

            ResultCommand = $"Объем стен {Math.Round(result, 2)} м3";
        }

        private void GetQuantityPipes()
        {
            Document document = _commandData.Application.ActiveUIDocument.Document;

            var pipes = new FilteredElementCollector(document)
                .WhereElementIsNotElementType()
                .OfCategory(BuiltInCategory.OST_PipeCurves)
                .Cast<Pipe>()
                .ToList();

            ResultCommand = $"Количество труб: {pipes.Count} шт.";
        }


    }
}
