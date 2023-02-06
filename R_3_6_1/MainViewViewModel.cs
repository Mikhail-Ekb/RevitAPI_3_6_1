using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPI_3_6_1
{
    public class MainViewViewModel
        private ExternalCommandData _commandData;
        public List<MechanicalSystemType> SystemTypes { get; } = new List<MechanicalSystemType>();
        public MechanicalSystemType SelectedDuctSystemType { get; set; }
        public List<DuctType> DuctTypes { get; } = new List<DuctType>();
        public DuctType SelectedDuctType { get; set; }
        public List<Level> Levels { get; } = new List<Level>();
        public Level SelectedLevel { get; set; }
        public double Hight { get; set; }
        public DelegateCommand SaveCommand { get; }
        public List<XYZ> Points { get; } = new List<XYZ>();

        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commandData = commandData;
            SystemTypes = DuctUtils.GetSystemTipes(commandData);
            DuctTypes = DuctUtils.GetDuctTipes(commandData);
            Levels = LevelUtils.GetLevels(commandData);
            Hight = 1200;
            SaveCommand = new DelegateCommand(OnSaveCommand);
            Points = SelectionUtils.GetPoint(_commandData, "Выберите точки", ObjectSnapTypes.Endpoints);
        }

        public event EventHandler HideRequest;
        private void RaiseHideRequest()
        {
            HideRequest?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler ShowRequest;
        private void RaiseShowRequest()
        {
            ShowRequest?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler CloseRequest;
        private void RaiseCloseRequest()
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }

        private void OnSaveCommand()
        {
            UIApplication uiapp = _commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            if(Points.Count<2|| DuctTypes==null|| Levels==null)
            {
                return;
            }
            var points = new List<Point>();

            var startPoint = new XYZ(Points[0].X, Points[0].Y, Points[0].Z + Hight);
            var endPoint = new XYZ(Points[1].X, Points[1].Y, Points[1].Z + Hight);

            for(int i=0; i<Points.Count; i++)
            {
                if (i == 0)
                    continue;                
            }

            using (var ts = new Transaction(doc, "Создание воздуховода"))
            {
                ts.Start();
                Duct.CreatePlaceholder(doc,
                    SelectedDuctSystemType.Id,
                    SelectedDuctType.Id, SelectedLevel.Id,
                    startPoint,
                    endPoint);
                ts.Commit();
            }    
        }
    }

}
