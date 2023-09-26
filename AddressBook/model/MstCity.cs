using System;
using System.Collections.Generic;

namespace AddressBook.model;

/// <summary>
/// 市区町村名マスタ
/// </summary>
public partial class MstCity
{
    /// <summary>
    /// 行政区域コード
    /// </summary>
    public string GovCode { get; set; } = null!;

    /// <summary>
    /// 都道府県名
    /// </summary>
    public string Pref { get; set; } = null!;

    /// <summary>
    /// 市区町村名
    /// </summary>
    public string City { get; set; } = null!;

    public virtual ICollection<MstZip> MstZips { get; set; } = new List<MstZip>();
}
