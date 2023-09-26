using System;
using System.Collections.Generic;

namespace AddressBook.model;

/// <summary>
/// 郵便番号マスタ
/// </summary>
public partial class MstZip
{
    /// <summary>
    /// ＩＤ
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 行政区域コード
    /// </summary>
    public string GovCode { get; set; } = null!;

    /// <summary>
    /// 郵便番号
    /// </summary>
    public string ZipCode { get; set; } = null!;

    /// <summary>
    /// 町域名
    /// </summary>
    public string Town { get; set; } = null!;

    public virtual MstCity GovCodeNavigation { get; set; } = null!;
}
