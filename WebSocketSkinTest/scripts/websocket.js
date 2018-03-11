'use strict';

var defaultIP = "192.168.187.231";
var connection = null;

function init() 
{ 
    var inputBox = document.getElementById("wsurladdress");
    
    if (inputBox != null)
        inputBox.value = defaultIP;
}  

function getBoundString(msg, startChar, stopChar)
{
    var response = "";
        
    if (msg != null && msg.length > 0)
    {
        var start = msg.indexOf(startChar);
            
        if (start >= 0)
        {
            start += startChar.length;
                
            var end = msg.indexOf(stopChar, start);
            
            if (start < end)
            {
                response = msg.substring(start, end);
            }
        }
    }
        
    return response;
}

function getBoundString_EndLastIndex(msg, startChar, stopChar)
{
    var response = "";
        
    if (msg != null && msg.length > 0)
    {
        var start = msg.indexOf(startChar);
            
        if (start >= 0)
        {
            start += startChar.length;
                
            var end = msg.lastIndexOf(stopChar);
            
            if (start < end)
            {
                response = msg.substring(start, end);
            }
        }
    }
        
    return response;
}	


function startWebsocket() 
{ 
    var ip = document.getElementById("wsurladdress").value;
    
    if (ip != null)
    {
        var wsUri = "ws://" + ip + ":8080/";
        
        websocket = new WebSocket(wsUri); 
        
        websocket.onopen = function(evt) 
        { 
            onOpen(evt) 
        }; 
        
        websocket.onclose = function(evt) 
        { 
            onClose(evt) 
        }; 
        
        websocket.onmessage = function(evt) 
        { 
            onMessage(evt) 
        }; 
        
        websocket.onerror = function(evt) 
        { 
            onError(evt) 
        };
    }		
}  

function onOpen(evt) 
{ 
    connection = document.getElementById("connection");
    
    if (connection != null)
        connection.innerHTML = "WebSocket: Connected";
}  

function onClose(evt) 
{ 
    connection = document.getElementById("connection");
    
    if (connection != null)
        connection.innerHTML = "WebSocket: Disconnected";
}  

function onMessage(evt) 
{ 
    if (evt != null)
    {
        var msg = evt.data;
        
        //ON[CHANNEL]
        if (msg.indexOf("ON[") == 0)
        {
            var channel = parseInt(getBoundString(msg, "ON[", "]"), 10);
            
            if (isNaN(channel) == false)
            {
                var button = document.getElementById("fb" + channel);
                
                if (button != null)
                    button.style.background = "green";
            }
        }
        //OFF[CHANNEL]
        else if (msg.indexOf("OFF[") == 0)
        {
            var channel = parseInt(getBoundString(msg, "OFF[", "]"), 10);

            if (isNaN(channel) == false)
            {
                var button = document.getElementById("fb" + channel);
                
                if (button != null)
                    button.style.background = "";
            }
        }
        // LEVEL[LEVEL,VALUE]
        else if (msg.indexOf("LEVEL[") == 0)
        {
            var level = parseInt(getBoundString(msg, "LEVEL[", ","), 10);
            var value = parseInt(getBoundString(msg, ",", "]"), 10);					

            // set slider level
            var slider = document.getElementById("sliderInput" + level);
            
            if (slider != null)
                slider.value = value;
            
            // set feedback text
            var text = document.getElementById("analogValue" + level);
            
            if (text != null)
                text.innerHTML = "" + value;
        }
        // STRING[SIG,DATA]
        else if (msg.indexOf("STRING[") == 0)
        {
            var text = parseInt(getBoundString(msg, "STRING[", ","), 10);
            var value = getBoundString_EndLastIndex(msg, ",", "]");					
                            
            // set receiving text
            var other = document.getElementById("text" + text);
            
            if (other != null)
                other.innerHTML = "Text" + text + ": " + value;
        }
    }
}  

function onError(evt) 
{ 
}  

function doSend(message) 
{ 
    websocket.send(message); 
}  

function socketclose()
{
    websocket.close();
}

function doPush(channel)
{
    doSend("PUSH[" + channel + "]");
}

function doRelease(channel)
{
    doSend("RELEASE[" + channel + "]");
}

function sendLevel(sig)
{
    var inputRange = document.getElementById("sliderInput" + sig);

    if (inputRange != null)
        doSend("LEVEL[" + sig + "," + inputRange.value + "]");
}

function sendString(sig)
{
    var inputText = document.getElementById("stringInput" + sig);
    
    if (inputText != null)
        doSend("STRING[" + sig + "," + inputText.value + "]");
}

window.addEventListener("load", init, false);  