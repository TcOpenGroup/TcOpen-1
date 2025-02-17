﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;

namespace TcoIo
{
    public class WiringObject
    {
        public enum ConectionType
        {
            K2K,        //direct E-bus connection to the previous E-Bus terminal (no wire)
            Y20,        //direct EtherCAT connection to the previous EtherCAT device of type Y, KY or YY that is not visible (wire shape ----)
            Y2Y,        //direct EtherCAT connection to the previous EtherCAT device of type Y, KY or YY (wire shape ----)
            Y2YKY,      //EtherCAT connection to the previous EtherCAT device of type YKY (wire shape C)
            Y2KYKY_X1,  //EtherCAT connection to the previous EtherCAT device of type KYKY X1 (wire shape L)
            Y2KYKY_X2,  //EtherCAT connection to the previous EtherCAT device of type KYKY X2 (wire shape L)
        }

        private ConectionType wiringType;
        public ConectionType WiringType
        {
            get { return this.wiringType; }
            set { this.wiringType = value ; }
        }

        
        private Path path;
        public Path Path
        {
            get { return this.path ?? new Path(); }
            set { this.path = value ?? new Path(); }
        }


        public WiringObject()
        {
            WiringType = ConectionType.K2K;
            Path = new Path();
        }

    }
}
