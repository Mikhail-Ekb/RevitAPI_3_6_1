using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_3_6_1
{
    public class SelectionUtils
    {
        // Создаю статический класс, который можно вызывать без создания нового экземпляра класса
        public static List<XYZ> GetPoint(ExternalCommandData commandData, string promtMessage, ObjectSnapTypes objectSnapTypes)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;

            List<XYZ> point = new List<XYZ>();

            //цикла do цикл while сразу проверяет истинность некоторого условия, и если условие истинно, то код цикла выполняется
            // Вначале проверяем что 
            while (true)
            {
                XYZ pickedPoint = null;
                try
                {
                    pickedPoint = uidoc.Selection.PickPoint(objectSnapTypes, promtMessage);
                }
                catch (Autodesk.Revit.Exceptions.OperationCanceledException ex)
                {
                    break;
                }
                point.Add(pickedPoint);
            }
            return point;
        }
    }
}
