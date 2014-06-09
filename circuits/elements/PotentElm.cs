using System;
using System.Collections;
using System.Collections.Generic;

namespace Circuits {

	public class PotentElm : CircuitElement {

		public double position, maxResistance, resistance1, resistance2;
		public double current1, current2, current3;

		public ElementLead lead2;

		public PotentElm(CirSim s) : base(s) {
			lead2 = new ElementLead(this,2);
			setup();
			maxResistance = 1000;
			position = 0.5;
		}

		public void setup() { }

		public override int getLeadCount() {
			return 3;
		}

		public override ElementLead getLead(int n) {
			return (n == 0) ? lead0 : (n == 1) ? lead1 : lead2;
		}

		public override void calculateCurrent() {
			current1 = (volts[0] - volts[2]) / resistance1;
			current2 = (volts[1] - volts[2]) / resistance2;
			current3 = -current1 - current2;
		}

		public override void stamp() {
			resistance1 = maxResistance * position;
			resistance2 = maxResistance * (1 - position);
			sim.stampResistor(nodes[0], nodes[2], resistance1);
			sim.stampResistor(nodes[2], nodes[1], resistance2);
		}

		public override void getInfo(String[] arr) {
			arr[0] = "potentiometer";
			arr[1] = "Vd = " + getVoltageDText(getVoltageDiff());
			arr[2] = "R1 = " + getUnitText(resistance1, CirSim.ohmString);
			arr[3] = "R2 = " + getUnitText(resistance2, CirSim.ohmString);
			arr[4] = "I1 = " + getCurrentDText(current1);
			arr[5] = "I2 = " + getCurrentDText(current2);
		}

		/*public EditInfo getEditInfo(int n) {
			// ohmString doesn't work here on linux
			if (n == 0) {
				return new EditInfo("Resistance (ohms)", maxResistance, 0, 0);
			}
			if (n == 1) {
				EditInfo ei = new EditInfo("Slider Text", 0, -1, -1);
				ei.text = sliderText;
				return ei;
			}
			return null;
		}

		public void setEditValue(int n, EditInfo ei) {
			if (n == 0) {
				maxResistance = ei.value;
			}
			if (n == 1) {
				sliderText = ei.textf.getText();
				label.setText(sliderText);
			}
		}*/
	}
}