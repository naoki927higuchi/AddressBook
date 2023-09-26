using System;
using System.Collections.Generic;

namespace AddressBook.model;

/// <summary>
/// 住所マスタ
/// </summary>
public partial class MstAddress
{
    /// <summary>
    /// ＩＤ
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 全国地方公共団体コード
    /// </summary>
    public string GovCode { get; set; } = null!;

    /// <summary>
    /// 郵便番号（旧）
    /// </summary>
    public string ZipCodeOld { get; set; } = null!;

    /// <summary>
    /// 郵便番号
    /// </summary>
    public string ZipCode { get; set; } = null!;

    /// <summary>
    /// 都道府県名（カナ）
    /// </summary>
    public string PrefKana { get; set; } = null!;

    /// <summary>
    /// 市区町村名（カナ）
    /// </summary>
    public string CityCana { get; set; } = null!;

    /// <summary>
    /// 町域名（カナ）
    /// </summary>
    public string TownKana { get; set; } = null!;

    /// <summary>
    /// 都道府県名
    /// </summary>
    public string Pref { get; set; } = null!;

    /// <summary>
    /// 市区町村名
    /// </summary>
    public string City { get; set; } = null!;

    /// <summary>
    /// 町域名
    /// </summary>
    public string Town { get; set; } = null!;

    /// <summary>
    /// コード１
    /// </summary>
    public int Code1 { get; set; }

    /// <summary>
    /// コード２
    /// </summary>
    public int Code2 { get; set; }

    /// <summary>
    /// コード３
    /// </summary>
    public int Code3 { get; set; }

    /// <summary>
    /// コード４
    /// </summary>
    public int Code4 { get; set; }

    /// <summary>
    /// コード５
    /// </summary>
    public int Code5 { get; set; }

    /// <summary>
    /// コード６
    /// </summary>
    public int Code6 { get; set; }
}
