
using System;
using System.Text;
using Crestron.SimplSharp;                                         // For Basic SIMPL# Classes
using Newtonsoft.Json;                                          //Thanks to Neil Colvin. Full Library @ http://www.nivloc.com/downloads/crestron/SSharp/
using Crestron.SimplSharp.CrestronIO;
using System.Collections.Generic;


namespace TESTjsonConfig
{


    public class Config
    {
            public string RmName;                   //Room Name
            public string RmId;                     //Room Identifier
            public string RmHostname;               //Hostname
            public string DspHostnameIP;            //DSP hostname or IP
            public string DspPassword;              //DSP Password
            public string DspDialerPhone;           //DSP Dialer Phone Number
            public string VtcInboundNumber;         //VTC Inbound Dial Number
            public string[] DspExpanderHostname;    //DSP Expaner Hostname
            public int DspExpandersCount;           //Total number of expanders found. I'm passing this back to SIMPL+ to make the loop dynamic.
        //Hello








        /*Pass the FilePath from SIMPL+ then read in the file.
        Create the JSON Object and use the library to deserialize it into 
        our classes.
        */
        public void Reader(string FilePath)
        {


            string DaString;


            if (File.Exists(FilePath))       //Ok make sure the file is there
            {
                StreamReader daFile = new StreamReader(FilePath);
                DaString = daFile.ReadToEnd();
                daFile.Close();
            }
            else
            {
                CrestronConsole.PrintLine("File Not found\n\r");    //Generate error
                DaString = "";


            }


            Configuration Obj = JsonConvert.DeserializeObject<Configuration>(DaString); //All the heavy lifting


            DspExpanderHostname = new string[5];


            this.RmName                 = Obj.RoomName;
            this.RmId                   = Obj.RoomID;
            this.RmHostname             = Obj.RoomHostname;
            this.DspHostnameIP          = Obj.DSPConfig.Hostname;
            this.DspPassword            = Obj.DSPConfig.Password;
            this.DspDialerPhone         = Obj.DSPConfig.ATCPhoneNumber;
            this.VtcInboundNumber       = Obj.VTCConfig.InboundNumber;


            DspExpandersCount = Obj.DSPConfig.Expanders.Count;


            for (int i = 0; i < DspExpandersCount; i++) //fill in the arrays
            {
                DspExpanderHostname[i] = Obj.DSPConfig.Expanders[i].Hostname;
            }
        }






        /*Classes built from http://jsonutils.com/
         */


        /*Sources is an array of 24 entries. Can add properties here
         example:
"Sources": [
    {
        "Name": "Cable 1",
        "isUsing": 1,
        "Type": 1
    },{
        "Name": "Cable 2",
        "isUsing": 1,
        "Type": 1
    },{
        "Name": "Cable 3",
        "isUsing": 0,
        "Type": 1
    },{
        "Name": "Cable 4",
        "isUsing": 0,
        "Type": 1
    },{
        "Name": "Apple TV 1",
        "isUsing": 1,
        "Type": 3
         */
        public class DSPExpander
        {
            [JsonProperty("Hostname")]
            public string Hostname { get; set; }
        }




        public class DSPConfig
        {
            [JsonProperty("Hostname-IP")]
            public string Hostname { get; set; }
            [JsonProperty("Password")]
            public string Password { get; set; }
            [JsonProperty("ATC")]
            public ATCConfig ATCConfig { get; set; }
            [JsonProperty("Phone Number")]
            public string ATCPhoneNumber { get; set; }
            [JsonProperty("Expanders")]
            public IList<DSPExpander> Expanders { get; set; }
            
        }


        public class VTCConfig
        {
            [JsonProperty("Inbound Number")]
            public string InboundNumber { get; set; }
        }


        public class ATCConfig
        {
            [JsonProperty("Phone Number")]
            public string PhoneNumber { get; set; }
        }


        /* This is the main object
        This tells the JsonConvert where to put everything  
        */
        public class Configuration
        {
            [JsonProperty("Room Name")]
            public string RoomName { get; set; }
            [JsonProperty("Room ID")]
            public string RoomID { get; set; }
            [JsonProperty("Room Hostname")]
            public string RoomHostname { get; set; }
            [JsonProperty("DSP Configuration")]
            public DSPConfig DSPConfig { get; set; }
            [JsonProperty("VTC Configuration")]
            public VTCConfig VTCConfig { get; set; }
        }


    }
}
