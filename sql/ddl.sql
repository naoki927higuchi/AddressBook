-- Project Name : 住所録
-- Date/Time    : 2023/09/27 5:13:22
-- Author       : naoki
-- RDBMS Type   : Microsoft SQL Server 2016 ～
-- Application  : A5:SQL Mk-2

-- 住所マスタ
drop table if exists mst_address;

create table mst_address (
  id INT not null IDENTITY(1,1)
  , gov_code NVARCHAR(5) not null
  , zip_code_old NVARCHAR(5) not null
  , zip_code NVARCHAR(7) not null
  , pref_kana NVARCHAR(10) not null
  , city_cana NVARCHAR(40) not null
  , town_kana NVARCHAR(400) not null
  , pref NVARCHAR(10) not null
  , city NVARCHAR(20) not null
  , town NVARCHAR(400) not null
  , code1 INT not null
  , code2 INT not null
  , code3 INT not null
  , code4 INT not null
  , code5 INT not null
  , code6 INT not null
  , constraint mst_address_PKC primary key (id)
) ;

-- 市外局番
drop table if exists mst_area_code;

create table mst_area_code (
  id INT not null IDENTITY(1,1)
  , section_code NVARCHAR(8) not null
  , section NVARCHAR(800) not null
  , area_code NVARCHAR(4) not null
  , city_code NVARCHAR(4) not null
  , constraint mst_area_code_PKC primary key (id)
) ;

-- 行政区域コードマスタ
drop table if exists mst_gov_code;

create table mst_gov_code (
  id INT not null IDENTITY(1,1)
  , gov_code NVARCHAR(5)
  , pref NVARCHAR(10)
  , city NVARCHAR(20)
  , pref_kana NVARCHAR(10)
  , city_cana NVARCHAR(40)
  , rev NVARCHAR(20)
  , rev_date DATE
  , rev_code NVARCHAR(5)
  , rev_name NVARCHAR(20)
  , rev_name_kana NVARCHAR(20)
  , rev_reason NVARCHAR(40)
  , constraint mst_gov_code_PKC primary key (id)
) ;

-- 郵便番号マスタ
drop table if exists mst_zip;

create table mst_zip (
  id INT not null IDENTITY(1,1)
  , gov_code NVARCHAR(5) not null
  , zip_code NVARCHAR(7) not null
  , town NVARCHAR(40) not null
  , constraint mst_zip_PKC primary key (id)
) ;

-- 住所録トラン
drop table if exists trn_address;

create table trn_address (
  id INT not null IDENTITY(1,1)
  , name NVARCHAR(20) not null
  , zip_code NVARCHAR(7) not null
  , pref NVARCHAR(10) not null
  , city NVARCHAR(20) not null
  , town NVARCHAR(40) not null
  , block NVARCHAR(40)
  , apart NVARCHAR(40)
  , tel NVARCHAR(13)
  , mail NVARCHAR(80)
  , memo NVARCHAR(256)
  , constraint trn_address_PKC primary key (id)
) ;

-- 市区町村名マスタ
drop table if exists mst_city;

create table mst_city (
  gov_code NVARCHAR(5) not null
  , pref NVARCHAR(10) not null
  , city NVARCHAR(20) not null
  , constraint mst_city_PKC primary key (gov_code)
) ;

alter table mst_zip
  add constraint mst_zip_FK1 foreign key (gov_code) references mst_city(gov_code)
  on delete cascade;

execute sp_addextendedproperty N'MS_Description', N'市外局番', N'SCHEMA', N'dbo', N'TABLE', N'mst_area_code', null, null;
execute sp_addextendedproperty N'MS_Description', N'ＩＤ', N'SCHEMA', N'dbo', N'TABLE', N'mst_area_code', N'COLUMN', N'id';
execute sp_addextendedproperty N'MS_Description', N'番号区画コード', N'SCHEMA', N'dbo', N'TABLE', N'mst_area_code', N'COLUMN', N'section_code';
execute sp_addextendedproperty N'MS_Description', N'番号区画', N'SCHEMA', N'dbo', N'TABLE', N'mst_area_code', N'COLUMN', N'section';
execute sp_addextendedproperty N'MS_Description', N'市外局番', N'SCHEMA', N'dbo', N'TABLE', N'mst_area_code', N'COLUMN', N'area_code';
execute sp_addextendedproperty N'MS_Description', N'市内局番', N'SCHEMA', N'dbo', N'TABLE', N'mst_area_code', N'COLUMN', N'city_code';

execute sp_addextendedproperty N'MS_Description', N'郵便番号マスタ', N'SCHEMA', N'dbo', N'TABLE', N'mst_zip', null, null;
execute sp_addextendedproperty N'MS_Description', N'ＩＤ', N'SCHEMA', N'dbo', N'TABLE', N'mst_zip', N'COLUMN', N'id';
execute sp_addextendedproperty N'MS_Description', N'行政区域コード', N'SCHEMA', N'dbo', N'TABLE', N'mst_zip', N'COLUMN', N'gov_code';
execute sp_addextendedproperty N'MS_Description', N'郵便番号', N'SCHEMA', N'dbo', N'TABLE', N'mst_zip', N'COLUMN', N'zip_code';
execute sp_addextendedproperty N'MS_Description', N'町域名', N'SCHEMA', N'dbo', N'TABLE', N'mst_zip', N'COLUMN', N'town';

execute sp_addextendedproperty N'MS_Description', N'行政区域コードマスタ', N'SCHEMA', N'dbo', N'TABLE', N'mst_gov_code', null, null;
execute sp_addextendedproperty N'MS_Description', N'ＩＤ', N'SCHEMA', N'dbo', N'TABLE', N'mst_gov_code', N'COLUMN', N'id';
execute sp_addextendedproperty N'MS_Description', N'行政区域コード', N'SCHEMA', N'dbo', N'TABLE', N'mst_gov_code', N'COLUMN', N'gov_code';
execute sp_addextendedproperty N'MS_Description', N'都道府県名（漢字）', N'SCHEMA', N'dbo', N'TABLE', N'mst_gov_code', N'COLUMN', N'pref';
execute sp_addextendedproperty N'MS_Description', N'市区町村名（漢字）', N'SCHEMA', N'dbo', N'TABLE', N'mst_gov_code', N'COLUMN', N'city';
execute sp_addextendedproperty N'MS_Description', N'都道府県名（ｶﾅ）', N'SCHEMA', N'dbo', N'TABLE', N'mst_gov_code', N'COLUMN', N'pref_kana';
execute sp_addextendedproperty N'MS_Description', N'市区町村名（ｶﾅ）', N'SCHEMA', N'dbo', N'TABLE', N'mst_gov_code', N'COLUMN', N'city_cana';
execute sp_addextendedproperty N'MS_Description', N'コードの改定区分', N'SCHEMA', N'dbo', N'TABLE', N'mst_gov_code', N'COLUMN', N'rev';
execute sp_addextendedproperty N'MS_Description', N'改正年月日', N'SCHEMA', N'dbo', N'TABLE', N'mst_gov_code', N'COLUMN', N'rev_date';
execute sp_addextendedproperty N'MS_Description', N'改正後のコード', N'SCHEMA', N'dbo', N'TABLE', N'mst_gov_code', N'COLUMN', N'rev_code';
execute sp_addextendedproperty N'MS_Description', N'改正後の名称', N'SCHEMA', N'dbo', N'TABLE', N'mst_gov_code', N'COLUMN', N'rev_name';
execute sp_addextendedproperty N'MS_Description', N'改正後の名称(ｶﾅ)', N'SCHEMA', N'dbo', N'TABLE', N'mst_gov_code', N'COLUMN', N'rev_name_kana';
execute sp_addextendedproperty N'MS_Description', N'改正事由等', N'SCHEMA', N'dbo', N'TABLE', N'mst_gov_code', N'COLUMN', N'rev_reason';

execute sp_addextendedproperty N'MS_Description', N'市区町村名マスタ', N'SCHEMA', N'dbo', N'TABLE', N'mst_city', null, null;
execute sp_addextendedproperty N'MS_Description', N'行政区域コード', N'SCHEMA', N'dbo', N'TABLE', N'mst_city', N'COLUMN', N'gov_code';
execute sp_addextendedproperty N'MS_Description', N'都道府県名', N'SCHEMA', N'dbo', N'TABLE', N'mst_city', N'COLUMN', N'pref';
execute sp_addextendedproperty N'MS_Description', N'市区町村名', N'SCHEMA', N'dbo', N'TABLE', N'mst_city', N'COLUMN', N'city';

execute sp_addextendedproperty N'MS_Description', N'住所録トラン', N'SCHEMA', N'dbo', N'TABLE', N'trn_address', null, null;
execute sp_addextendedproperty N'MS_Description', N'ＩＤ', N'SCHEMA', N'dbo', N'TABLE', N'trn_address', N'COLUMN', N'id';
execute sp_addextendedproperty N'MS_Description', N'氏名', N'SCHEMA', N'dbo', N'TABLE', N'trn_address', N'COLUMN', N'name';
execute sp_addextendedproperty N'MS_Description', N'郵便番号', N'SCHEMA', N'dbo', N'TABLE', N'trn_address', N'COLUMN', N'zip_code';
execute sp_addextendedproperty N'MS_Description', N'都道府県名', N'SCHEMA', N'dbo', N'TABLE', N'trn_address', N'COLUMN', N'pref';
execute sp_addextendedproperty N'MS_Description', N'市区町村名', N'SCHEMA', N'dbo', N'TABLE', N'trn_address', N'COLUMN', N'city';
execute sp_addextendedproperty N'MS_Description', N'町域名', N'SCHEMA', N'dbo', N'TABLE', N'trn_address', N'COLUMN', N'town';
execute sp_addextendedproperty N'MS_Description', N'番地等', N'SCHEMA', N'dbo', N'TABLE', N'trn_address', N'COLUMN', N'block';
execute sp_addextendedproperty N'MS_Description', N'アパート等', N'SCHEMA', N'dbo', N'TABLE', N'trn_address', N'COLUMN', N'apart';
execute sp_addextendedproperty N'MS_Description', N'電話番号', N'SCHEMA', N'dbo', N'TABLE', N'trn_address', N'COLUMN', N'tel';
execute sp_addextendedproperty N'MS_Description', N'メール', N'SCHEMA', N'dbo', N'TABLE', N'trn_address', N'COLUMN', N'mail';
execute sp_addextendedproperty N'MS_Description', N'メモ', N'SCHEMA', N'dbo', N'TABLE', N'trn_address', N'COLUMN', N'memo';

execute sp_addextendedproperty N'MS_Description', N'住所マスタ', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', null, null;
execute sp_addextendedproperty N'MS_Description', N'ＩＤ', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'id';
execute sp_addextendedproperty N'MS_Description', N'全国地方公共団体コード', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'gov_code';
execute sp_addextendedproperty N'MS_Description', N'郵便番号（旧）', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'zip_code_old';
execute sp_addextendedproperty N'MS_Description', N'郵便番号', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'zip_code';
execute sp_addextendedproperty N'MS_Description', N'都道府県名（カナ）', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'pref_kana';
execute sp_addextendedproperty N'MS_Description', N'市区町村名（カナ）', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'city_cana';
execute sp_addextendedproperty N'MS_Description', N'町域名（カナ）', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'town_kana';
execute sp_addextendedproperty N'MS_Description', N'都道府県名', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'pref';
execute sp_addextendedproperty N'MS_Description', N'市区町村名', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'city';
execute sp_addextendedproperty N'MS_Description', N'町域名', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'town';
execute sp_addextendedproperty N'MS_Description', N'コード１', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'code1';
execute sp_addextendedproperty N'MS_Description', N'コード２', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'code2';
execute sp_addextendedproperty N'MS_Description', N'コード３', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'code3';
execute sp_addextendedproperty N'MS_Description', N'コード４', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'code4';
execute sp_addextendedproperty N'MS_Description', N'コード５', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'code5';
execute sp_addextendedproperty N'MS_Description', N'コード６', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'code6';

