using Mastercam.IO;
using Mastercam.App.Types;
using Mastercam.Support;
using Mastercam.Operations.Types;
using Mastercam.Operations;
using System.Collections.Generic;
using Mastercam.Database;
using System;

namespace _SurfaceToolpathDepth
{
    public class SurfaceToolpathDepth : Mastercam.App.NetHook3App
    {
        public Mastercam.App.Types.MCamReturn SurfaceToolpathDepthRun(Mastercam.App.Types.MCamReturn notused){
            var maxDepth = 0.0;
            DialogManager.AskForNumber("Max Stepdown", ref maxDepth);
            var contourList = new List<int>();
            var pencilList = new List<int>();
            var selectedOps = SearchManager.GetOperations(true);
            var surfaceFinishContourOpID = Mastercam.IO.Interop.SelectionManager.GetOperations(15);
            var surfaceFinishPencilOpID = Mastercam.IO.Interop.SelectionManager.GetOperations(39);
            foreach (var op in surfaceFinishContourOpID)
            {
                foreach (var thisOp in selectedOps)
                {
                    var selectedOp = thisOp.GetOperationID();
                    if (selectedOp == op)
                    {
                        contourList.Add(selectedOp);
                    }
                }
            }
            foreach (var op in surfaceFinishPencilOpID)
            {
                foreach (var thisOp in selectedOps)
                {
                    var selectedOp = thisOp.GetOperationID();
                    if (selectedOp == op)
                    {
                        pencilList.Add(selectedOp);
                    }
                }
            }
            foreach (var op in contourList)
            {
                Mastercam.IO.Interop.SelectionManager.AlterContourFinish(Math.Abs(maxDepth), maxDepth, maxDepth);
                var thisOp = SearchManager.GetOperation(op);
                thisOp.MarkDirty();
                thisOp.Commit(false);
            }
            OperationsManager.RefreshOperationsManager(true);
            foreach (var op in pencilList)
            {
                Mastercam.IO.Interop.SelectionManager.AlterPencilFinish(true, maxDepth, 0.00);
                var thisOp = SearchManager.GetOperation(op);
                thisOp.MarkDirty();
                thisOp.Commit(false);
            }
            OperationsManager.RefreshOperationsManager(true);

            return MCamReturn.NoErrors;
        }
    }
}
