using System;
using System.Collections.Generic;

namespace AddressBook.model;

/// <summary>
/// 市外局番
/// </summary>
public partial class MstAreaCode
{
    /// <summary>
    /// ＩＤ
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 番号区画コード
    /// </summary>
    public string SectionCode { get; set; } = null!;

    /// <summary>
    /// 番号区画
    /// </summary>
    public string Section { get; set; } = null!;

    /// <summary>
    /// 市外局番
    /// </summary>
    public string AreaCode { get; set; } = null!;

    /// <summary>
    /// 市内局番
    /// </summary>
    public string CityCode { get; set; } = null!;
}
