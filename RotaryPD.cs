using Mastercam.IO;
using Mastercam.App.Types;
using Mastercam.Support;
using Mastercam.Operations.Types;
using Mastercam.Operations;
using System.Collections.Generic;
using Mastercam.Database;

namespace _RotaryPD
{
    public class RotaryPD : Mastercam.App.NetHook3App
    {
        public Mastercam.App.Types.MCamReturn RotaryPDRun(Mastercam.App.Types.MCamReturn notused){
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
                Mastercam.IO.Interop.SelectionManager.AlterContourFinish(0.2, -0.2, -0.2);
                var thisOp = SearchManager.GetOperation(op);
                thisOp.MarkDirty();
                thisOp.Commit(false);
            }
            OperationsManager.RefreshOperationsManager(true);
            foreach (var op in pencilList)
            {
                Mastercam.IO.Interop.SelectionManager.AlterPencilFinish(true, -0.020, 0.00);
                var thisOp = SearchManager.GetOperation(op);
                thisOp.MarkDirty();
                thisOp.Commit(false);
            }
            OperationsManager.RefreshOperationsManager(true);

            return MCamReturn.NoErrors;
        }
    }
}
