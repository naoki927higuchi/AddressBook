using System;
using System.Collections.Generic;

namespace AddressBook.model;

/// <summary>
/// 都道府県マスタ
/// </summary>
public partial class MstPrefecture
{
    /// <summary>
    /// ＩＤ
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 都道府県名
    /// </summary>
    public string Pref { get; set; } = null!;

    /// <summary>
    /// 都道府県名（カナ）
    /// </summary>
    public string PrefKana { get; set; } = null!;
}
