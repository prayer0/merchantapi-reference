﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MerchantAPI.APIGateway.Test.Functional.CallbackWebServer;
using Microsoft.AspNetCore.Http;

namespace MerchantAPI.APIGateway.Test.Functional
{

  /// <summary>
  /// Traces all callbacks received through mocked call back server
  /// </summary>
  public class CallbackFunctionalTests : ICallbackReceived 
  {
    object lockObj = new object();

    List<(string path, string request)> calls = new List<(string path, string request)>();
    public string Url => "http://mockCallback:8321";


    public (string path, string request)[] Calls
    {
      get
      {
        lock (lockObj)
        {
          return calls.ToArray();
        }
      }
    }

    public Task CallbackReceivedAsync(string path, IHeaderDictionary headers, byte[] data)
    {

      lock (lockObj)
      {
        var str = new StreamReader(new MemoryStream(data));
        calls.Add((path, str.ReadToEnd()));
      }

      return Task.CompletedTask;
    }
  }
}
