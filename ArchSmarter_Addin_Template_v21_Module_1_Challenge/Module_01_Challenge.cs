#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB.Visual;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Media;
using System.Windows;

#endregion

namespace ArchSmarter_Addin_Template_v21_Module_1_Challenge
{
    [Transaction(TransactionMode.Manual)]
    public class Module_01_Challenge : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // this is a variable for the Revit application
            UIApplication uiapp = commandData.Application;

            // this is a variable for the current Revit model
            Document doc = uiapp.ActiveUIDocument.Document;

            // Your code goes here

            // Declare a number variable and set it to 250.

            int numberVar1 = 250;

            // Declare a starting elevation variable and set it to 0

            double startElevation1 = 0;

            // Declare a floor height variable and set it to 15

            double floorHeightVar = 15;

            Transaction t = new Transaction(doc);
            t.Start("Cool Tool");

            // Loop through the number 1 to the number variable
            // Create a level for each number

            for (int i = 0; i <= numberVar1; i++)
            {
                Level newLevel = Level.Create(doc, startElevation1);
                newLevel.Name = "Level " + i.ToString() + " F.F.";

                // After creating the level, increment the current elevation by the floor height value.

                startElevation1 += floorHeightVar;

                // If the number is divisible by both 3 and 5, create a sheet and name it "FIZZBUZZ_#"

                if (i % 3 == 0 && i % 5 == 0)
                {
                    FilteredElementCollector collector1 = new FilteredElementCollector(doc);
                    collector1.OfClass(typeof(ViewFamilyType));
                    ViewFamilyType floorPlanVFT = null;
                    foreach (ViewFamilyType curVFT in collector1)
                    {
                        if (curVFT.ViewFamily == ViewFamily.FloorPlan)
                        {
                            floorPlanVFT = curVFT;
                            ViewPlan newPlan = ViewPlan.Create(doc, floorPlanVFT.Id, newLevel.Id);
                            newPlan.Name = "FIZZ_#" + i.ToString();

                            FilteredElementCollector collector4 = new FilteredElementCollector(doc);
                            collector4.OfCategory(BuiltInCategory.OST_TitleBlocks);

                            ViewSheet newSheet = ViewSheet.Create(doc, collector4.FirstElementId());
                            newSheet.Name = "FIZZBUZZ_#" + i;
                            newSheet.SheetNumber = "A-" + i;

                            // BONUS! In addition to creating a sheet, create a floor plan for each
                            // FIZZBUZZ. Next, add the floor plan to the sheet by creating a Viewport element.

                            XYZ insPoint = new XYZ(1.5, 1, 0);

                            Viewport newViewport = Viewport.Create(doc, newSheet.Id, newPlan.Id, insPoint);
                            break;
                        }
                    }
                }
                // If the number is divisible by 3, create a floor plan and name it "FIZZ_#"  
                else if (i % 3 == 0)
                {
                    FilteredElementCollector collector1 = new FilteredElementCollector(doc);
                    collector1.OfClass(typeof(ViewFamilyType));
                    ViewFamilyType floorPlanVFT = null;
                    foreach (ViewFamilyType curVFT in collector1)
                    {
                        if (curVFT.ViewFamily == ViewFamily.FloorPlan)
                        {
                            floorPlanVFT = curVFT;
                            ViewPlan newPlan = ViewPlan.Create(doc, floorPlanVFT.Id, newLevel.Id);
                            newPlan.Name = "FIZZ_#" + i.ToString();
                            break;
                        }
                    }
                }

                // If the number is divisible by 5, create a ceiling plan and name it "BUZZ_#"

                if (i % 5 == 0)
                {
                    FilteredElementCollector collector2 = new FilteredElementCollector(doc);
                    collector2.OfClass(typeof(ViewFamilyType));
                    ViewFamilyType ceilingPlanVFT = null;
                    foreach (ViewFamilyType curVFT in collector2)
                    {
                        if (curVFT.ViewFamily == ViewFamily.CeilingPlan)
                        {
                            ceilingPlanVFT = curVFT;
                            ViewPlan newCeilingPlan = ViewPlan.Create(doc, ceilingPlanVFT.Id, newLevel.Id);
                            newCeilingPlan.Name = "BUZZ_#" + i.ToString();
                            break;
                        }
                    }
                }
            }
            t.Commit();
            t.Dispose();
           

        return Result.Succeeded;
        }
        internal static PushButtonData GetButtonData()
        {
            // use this method to define the properties for this command in the Revit ribbon
            string buttonInternalName = "btnCommand1";
            string buttonTitle = "Button 1";

            ButtonDataClass myButtonData1 = new ButtonDataClass(
                buttonInternalName,
                buttonTitle,
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                Properties.Resources.Blue_32,
                Properties.Resources.Blue_16,
                "This is a tooltip for Button 1");

            return myButtonData1.Data;
        }
    }
}
