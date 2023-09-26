using System;
using System.Collections.Generic;

namespace AddressBook.model;

/// <summary>
/// 住所録表示エンティティ
/// </summary>
public partial class ViewAddress
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
    public string Address { get; set; } = null!;

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
