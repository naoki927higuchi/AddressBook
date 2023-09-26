using System;
using System.Collections.Generic;

namespace AddressBook.model;

/// <summary>
/// 行政区域コードマスタ
/// </summary>
public partial class MstGovCode
{
    /// <summary>
    /// ＩＤ
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 行政区域コード
    /// </summary>
    public string? GovCode { get; set; }

    /// <summary>
    /// 都道府県名（漢字）
    /// </summary>
    public string? Pref { get; set; }

    /// <summary>
    /// 市区町村名（漢字）
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// 都道府県名（ｶﾅ）
    /// </summary>
    public string? PrefKana { get; set; }

    /// <summary>
    /// 市区町村名（ｶﾅ）
    /// </summary>
    public string? CityCana { get; set; }

    /// <summary>
    /// コードの改定区分
    /// </summary>
    public string? Rev { get; set; }

    /// <summary>
    /// 改正年月日
    /// </summary>
    public DateTime? RevDate { get; set; }

    /// <summary>
    /// 改正後のコード
    /// </summary>
    public string? RevCode { get; set; }

    /// <summary>
    /// 改正後の名称
    /// </summary>
    public string? RevName { get; set; }

    /// <summary>
    /// 改正後の名称(ｶﾅ)
    /// </summary>
    public string? RevNameKana { get; set; }

    /// <summary>
    /// 改正事由等
    /// </summary>
    public string? RevReason { get; set; }
}
