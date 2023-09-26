using System;
using System.Collections.Generic;

namespace AddressBook.model;

/// <summary>
/// 住所録トラン
/// </summary>
public partial class TrnAddress
{
    /// <summary>
    /// ＩＤ
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 氏名
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// 郵便番号
    /// </summary>
    public string ZipCode { get; set; } = null!;

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
    /// 番地等
    /// </summary>
    public string? Block { get; set; }

    /// <summary>
    /// アパート等
    /// </summary>
    public string? Apart { get; set; }

    /// <summary>
    /// 電話番号
    /// </summary>
    public string? Tel { get; set; }

    /// <summary>
    /// メール
    /// </summary>
    public string? Mail { get; set; }

    /// <summary>
    /// メモ
    /// </summary>
    public string? Memo { get; set; }
}
