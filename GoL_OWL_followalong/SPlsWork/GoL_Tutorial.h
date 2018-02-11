namespace GoL_Tutorial;
        // class declarations
         class GoL;
     class GoL 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION SetCellState ( SIGNED_LONG_INTEGER row , SIGNED_LONG_INTEGER column );
        FUNCTION Clear ();
        SIGNED_LONG_INTEGER_FUNCTION GetCellState ( SIGNED_LONG_INTEGER row , SIGNED_LONG_INTEGER column );
        FUNCTION NextGeneration ();
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

