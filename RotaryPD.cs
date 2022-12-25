using Mastercam.IO;
using Mastercam.App.Types;
using Mastercam.Support;
using Mastercam.Operations.Types;
using Mastercam.Operations;

namespace _RotaryPD
{
    public class RotaryPD : Mastercam.App.NetHook3App
    {
        public Mastercam.App.Types.MCamReturn RotaryPDRun(Mastercam.App.Types.MCamReturn notused){
            OperationsManager.UnSelectAllOperations();
            var surfaceFinishContourOpID = Mastercam.IO.Interop.SelectionManager.GetOperations(15);
            var surfaceFinishPencilOpID = Mastercam.IO.Interop.SelectionManager.GetOperations(39);
            foreach (var op in surfaceFinishContourOpID)
            {
                var thisOp = SearchManager.GetOperation(op);
                thisOp.SetSelectedState(true);
                Mastercam.IO.Interop.SelectionManager.AlterContourFinish(op, 0.2, -0.2, -0.2);
            }
            OperationsManager.RefreshOperationsManager(true);
            foreach (var op in surfaceFinishPencilOpID)
            {
                var thisOp = SearchManager.GetOperation(op);
                thisOp.SetSelectedState(true);
            }
            OperationsManager.RefreshOperationsManager(true);

            return MCamReturn.NoErrors;
        }
    }
}
