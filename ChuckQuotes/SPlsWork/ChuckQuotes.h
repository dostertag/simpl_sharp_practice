namespace ChuckQuotes;
        // class declarations
         class ChuckJoke;
         class ChuckSays;
         class Joke;
     class ChuckJoke 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetRandomJoke ();
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

     class ChuckSays 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING type[];
        Joke value;
    };

     class Joke 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        SIGNED_LONG_INTEGER id;
        STRING joke[];
        STRING categories[][];
    };

