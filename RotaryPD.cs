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
            void ChangeOperations(){
                var rotaryPD = 0.0;
                DialogManager.AskForNumber("Enter New Rotary PD", ref rotaryPD);
                OperationsManager.RefreshOperationsManager(true);
                var operationIDs = SearchManager.GetOperationIDs(true);
                var axisParams = new RotaryAxisParams
                {
                    Enabled = true,
                    Diameter = rotaryPD,
                    Type = RotaryAxisPositionType.AxisSubstitution,
                    Direction = RotaryAxisDirection.CW,
                    AxisSubstitution = RotaryAxisSubstitution.SubsituteY
                };

                foreach (var opID in operationIDs){
                    var op = SearchManager.GetOperation(opID);
                    op.RotaryAxis = axisParams;
                    op.MarkDirty();
                    op.Commit(false);
                }
                OperationsManager.RefreshOperationsManager(true);
            }
            ChangeOperations();
            return MCamReturn.NoErrors;
        }
    }
}
