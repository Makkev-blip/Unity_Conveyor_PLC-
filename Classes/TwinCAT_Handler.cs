using System;
using System.Globalization;
using TwinCAT.Ads;
using UnityEngine;
using UnityEngine.Serialization;

public class TwinCAT_Handler : MonoBehaviour
{
    private AdsClient _tcClient;
    public OS1 s1;
    public OS2 s2;
    public OS3 s3;

    public Part player;

    void Awake()
    {
        _tcClient = new AdsClient();
        _tcClient.Connect("192.168.11.110.1.1", 851);
        if (_tcClient.IsConnected)
        {
            Debug.Log("Twin CAT ADS port connected");
        }
        else
        {
            Debug.LogError("ADS Connection failed");
        }
    }
    

    private bool ReadBool(string gvl, string variableName)
    {
        try
        {
            var hVar = _tcClient.CreateVariableHandle(gvl +"."+variableName);
            var readVariable = _tcClient.ReadAny(hVar, typeof(bool));
            _tcClient.DeleteVariableHandle(hVar);
            return bool.Parse(readVariable.ToString());
        }
        catch (AdsErrorException e)
        {
            Debug.LogError("TC Error - reading BOOL failed");
            Debug.Log(e.Message);
            return false;
        }
    }

    private int ReadInt(string gvl, string variableName)
    {
        var value = 0;
        try
        {
            var hVar = _tcClient.CreateVariableHandle(gvl + "." + variableName);
            value = (int)_tcClient.ReadAny(hVar,typeof(int));
            _tcClient.DeleteVariableHandle(hVar);
        }
        catch (Exception e)
        {
            Debug.LogError("TC Error - reading INT failed");
            Debug.LogError(e.Message);
        }
        return value;
    }
    
    public double ReadDouble(string gvl, string variableName)
    {
        double value = 0.0;
        try
        { 
            var hVar = _tcClient.CreateVariableHandle(gvl + "." + variableName);
            value = (double)_tcClient.ReadAny(hVar,typeof(double));
            _tcClient.DeleteVariableHandle(hVar);
        }
        catch (Exception e)
        {
            Debug.LogError("TC Error - reading Double failed");
            Debug.LogError(e.Message);
        }
        return value;
    }
    
    // Write methods
    
    public bool WriteInt(string pou, string variableName, int value)
    {
        try
        {
            var hVar = _tcClient.CreateVariableHandle(pou + "." + variableName);
            _tcClient.WriteAny(hVar, value);
            _tcClient.DeleteVariableHandle(hVar);
            return true;
        }
        catch (AdsErrorException exc)
        {
            Debug.LogError("TC Write Error " + exc.Message);
        }
        return false;
    }
    public bool WriteDouble(string gvl, string variableName, double value)
    {
        try
        {
            var hVar = _tcClient.CreateVariableHandle(gvl + "." + variableName);
            _tcClient.WriteAny(hVar,variableName);
            _tcClient.DeleteVariableHandle(hVar);
            return true;
        }
        catch (AdsErrorException exc)
        {
            Debug.LogError("TC Write Error " + exc.Message);
        }
        return false;
    }

    private bool WriteBool(string gvl, string variableName, bool value)
    {
        try
        {
            var hVar = _tcClient.CreateVariableHandle(gvl + "." + variableName);
            _tcClient.WriteAny(hVar, value);
            _tcClient.DeleteVariableHandle(hVar);
            return true;
        }
        catch (Exception exc)
        {
            Debug.LogError("TC Write Error " + exc.Message);
        }
        return false;
    }
    
    
    void Update()
    {
        if (_tcClient.IsConnected)
        {
            
            
            //Inputs from PLC
            player.nVel = (float)ReadDouble("DataExchange", "nVelocity");
            player.nAcc = (float)ReadDouble("DataExchange", "nAcceleration");
            player.nDec = (float)ReadDouble("DataExchange", "nDeceleration");
            player.nDir = ReadInt("DataExchange", "nDirection");
            player.bExecute = ReadBool("DataExchange", "bAxis");
            player.bHalt = ReadBool("DataExchange", "bHalt");
            

            Debug.Log(string.Format("Vel: {0}, Acc: {1}, Dec: {2}, Direction: {3}, Axis: {4}, Halt: {5}",
                player.nVel.ToString(CultureInfo.CurrentCulture), 
                player.nAcc.ToString(CultureInfo.CurrentCulture),
                player.nDec.ToString(CultureInfo.CurrentCulture),
                player.nDir.ToString(CultureInfo.InvariantCulture),
                player.bExecute.ToString(CultureInfo.CurrentCulture),
                player.bHalt.ToString(CultureInfo.CurrentCulture)));


            //OUTPUTS TO PLC
            
                var ps1 = this.s1.bS1;
                var ps2 = this.s2.bS2;
                var ps3 = this.s3.bS3;
                var inVelocity = player.bInVelocity;

                bool writebS1 = WriteBool("DataExchange", "bS1", ps1);
                bool writebS2 = WriteBool("DataExchange", "bS2", ps2);
                bool writebS3 = WriteBool("DataExchange", "bS3", ps3);
                bool writedVel = WriteBool("DataExchange", "bInVelocity", inVelocity);
                

        }
    }
}



