﻿// Copyright (c) 2020 Bitcoin Association

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MerchantAPI.APIGateway.Domain.Models
{
  public class FeeAmount : IValidatableObject 
  {

    public static class AmountType
    {
      public const string MiningFee = "MiningFee";
      public const string RelayFee = "RelayFee";     
    }

    public string FeeAmountType { get; set; }

    public long Satoshis { get; set; }

    public long Bytes { get; set; }

    public bool IsMiningFee()
    {
      return FeeAmountType == AmountType.MiningFee;
    }

    public bool IsRelayFee()
    {
      return FeeAmountType == AmountType.RelayFee;
    }


    public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (Satoshis < 0 || Bytes < 0)
      {
        yield return new ValidationResult($"FeeAmount: value for {nameof(Satoshis)} and {nameof(Bytes)} must be non negative.");
      }
      if (string.IsNullOrEmpty(FeeAmountType))
      {
        yield return new ValidationResult($"FeeAmount: {nameof(FeeAmountType)} is undefined.");
      }
    }
  }
}
