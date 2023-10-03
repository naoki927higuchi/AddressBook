-- Project Name : 住所録
-- Date/Time    : 2023/10/04 4:19:55
-- Author       : naoki
-- RDBMS Type   : Microsoft SQL Server 2016 ～
-- Application  : A5:SQL Mk-2

-- 住所マスタ
DROP TABLE if exists mst_address;

CREATE TABLE mst_address (
  id INT NOT NULL IDENTITY(1,1)
  , gov_code NVARCHAR(5) NOT NULL
  , zip_code_old NVARCHAR(5) NOT NULL
  , zip_code NVARCHAR(7) NOT NULL
  , pref_kana NVARCHAR(10) NOT NULL
  , city_cana NVARCHAR(40) NOT NULL
  , town_kana NVARCHAR(400) NOT NULL
  , pref NVARCHAR(10) NOT NULL
  , city NVARCHAR(20) NOT NULL
  , town NVARCHAR(400) NOT NULL
  , code1 INT NOT NULL
  , code2 INT NOT NULL
  , code3 INT NOT NULL
  , code4 INT NOT NULL
  , code5 INT NOT NULL
  , code6 INT NOT NULL
  , CONSTRAINT mst_address_PKC PRIMARY KEY (id)
) ;

-- 市外局番
DROP TABLE if exists mst_area_code;

CREATE TABLE mst_area_code (
  id INT NOT NULL IDENTITY(1,1)
  , section_code NVARCHAR(8) NOT NULL
  , section NVARCHAR(800) NOT NULL
  , area_code NVARCHAR(4) NOT NULL
  , city_code NVARCHAR(4) NOT NULL
  , CONSTRAINT mst_area_code_PKC PRIMARY KEY (id)
) ;

-- 都道府県マスタ
DROP TABLE if exists mst_prefecture;

CREATE TABLE mst_prefecture (
  id INT NOT NULL IDENTITY(1,1)
  , pref NVARCHAR(10) NOT NULL
  , pref_kana NVARCHAR(10) NOT NULL
  , CONSTRAINT mst_prefecture_PKC PRIMARY KEY (id)
) ;

-- 郵便番号マスタ
DROP TABLE if exists mst_zip;

CREATE TABLE mst_zip (
  id INT NOT NULL IDENTITY(1,1)
  , gov_code NVARCHAR(5) NOT NULL
  , zip_code NVARCHAR(7) NOT NULL
  , town NVARCHAR(40) NOT NULL
  , CONSTRAINT mst_zip_PKC PRIMARY KEY (id)
) ;

-- 住所録トラン
DROP TABLE if exists trn_address;

CREATE TABLE trn_address (
  id INT NOT NULL IDENTITY(1,1)
  , name NVARCHAR(20) NOT NULL
  , zip_code NVARCHAR(7) NOT NULL
  , pref NVARCHAR(10) NOT NULL
  , city NVARCHAR(20) NOT NULL
  , town NVARCHAR(40) NOT NULL
  , block NVARCHAR(40)
  , apart NVARCHAR(40)
  , tel NVARCHAR(13)
  , mail NVARCHAR(80)
  , memo NVARCHAR(256)
  , CONSTRAINT trn_address_PKC PRIMARY KEY (id)
) ;

-- 市区町村名マスタ
DROP TABLE if exists mst_city;

CREATE TABLE mst_city (
  gov_code NVARCHAR(5) NOT NULL
  , pref NVARCHAR(10) NOT NULL
  , city NVARCHAR(20) NOT NULL
  , CONSTRAINT mst_city_PKC PRIMARY KEY (gov_code)
) ;

ALTER TABLE mst_zip
  ADD CONSTRAINT mst_zip_FK1 FOREIGN KEY (gov_code) REFERENCES mst_city(gov_code)
  on delete cascade;

EXECUTE sp_addextendedproperty N'MS_Description', N'都道府県マスタ', N'SCHEMA', N'dbo', N'TABLE', N'mst_prefecture', NULL, NULL;
EXECUTE sp_addextendedproperty N'MS_Description', N'ＩＤ', N'SCHEMA', N'dbo', N'TABLE', N'mst_prefecture', N'COLUMN', N'id';
EXECUTE sp_addextendedproperty N'MS_Description', N'都道府県名', N'SCHEMA', N'dbo', N'TABLE', N'mst_prefecture', N'COLUMN', N'pref';
EXECUTE sp_addextendedproperty N'MS_Description', N'都道府県名（カナ）', N'SCHEMA', N'dbo', N'TABLE', N'mst_prefecture', N'COLUMN', N'pref_kana';

EXECUTE sp_addextendedproperty N'MS_Description', N'市外局番', N'SCHEMA', N'dbo', N'TABLE', N'mst_area_code', NULL, NULL;
EXECUTE sp_addextendedproperty N'MS_Description', N'ＩＤ', N'SCHEMA', N'dbo', N'TABLE', N'mst_area_code', N'COLUMN', N'id';
EXECUTE sp_addextendedproperty N'MS_Description', N'番号区画コード', N'SCHEMA', N'dbo', N'TABLE', N'mst_area_code', N'COLUMN', N'section_code';
EXECUTE sp_addextendedproperty N'MS_Description', N'番号区画', N'SCHEMA', N'dbo', N'TABLE', N'mst_area_code', N'COLUMN', N'section';
EXECUTE sp_addextendedproperty N'MS_Description', N'市外局番', N'SCHEMA', N'dbo', N'TABLE', N'mst_area_code', N'COLUMN', N'area_code';
EXECUTE sp_addextendedproperty N'MS_Description', N'市内局番', N'SCHEMA', N'dbo', N'TABLE', N'mst_area_code', N'COLUMN', N'city_code';

EXECUTE sp_addextendedproperty N'MS_Description', N'郵便番号マスタ', N'SCHEMA', N'dbo', N'TABLE', N'mst_zip', NULL, NULL;
EXECUTE sp_addextendedproperty N'MS_Description', N'ＩＤ', N'SCHEMA', N'dbo', N'TABLE', N'mst_zip', N'COLUMN', N'id';
EXECUTE sp_addextendedproperty N'MS_Description', N'行政区域コード', N'SCHEMA', N'dbo', N'TABLE', N'mst_zip', N'COLUMN', N'gov_code';
EXECUTE sp_addextendedproperty N'MS_Description', N'郵便番号', N'SCHEMA', N'dbo', N'TABLE', N'mst_zip', N'COLUMN', N'zip_code';
EXECUTE sp_addextendedproperty N'MS_Description', N'町域名', N'SCHEMA', N'dbo', N'TABLE', N'mst_zip', N'COLUMN', N'town';

EXECUTE sp_addextendedproperty N'MS_Description', N'市区町村名マスタ', N'SCHEMA', N'dbo', N'TABLE', N'mst_city', NULL, NULL;
EXECUTE sp_addextendedproperty N'MS_Description', N'行政区域コード', N'SCHEMA', N'dbo', N'TABLE', N'mst_city', N'COLUMN', N'gov_code';
EXECUTE sp_addextendedproperty N'MS_Description', N'都道府県名', N'SCHEMA', N'dbo', N'TABLE', N'mst_city', N'COLUMN', N'pref';
EXECUTE sp_addextendedproperty N'MS_Description', N'市区町村名', N'SCHEMA', N'dbo', N'TABLE', N'mst_city', N'COLUMN', N'city';

EXECUTE sp_addextendedproperty N'MS_Description', N'住所録トラン', N'SCHEMA', N'dbo', N'TABLE', N'trn_address', NULL, NULL;
EXECUTE sp_addextendedproperty N'MS_Description', N'ＩＤ', N'SCHEMA', N'dbo', N'TABLE', N'trn_address', N'COLUMN', N'id';
EXECUTE sp_addextendedproperty N'MS_Description', N'氏名', N'SCHEMA', N'dbo', N'TABLE', N'trn_address', N'COLUMN', N'name';
EXECUTE sp_addextendedproperty N'MS_Description', N'郵便番号', N'SCHEMA', N'dbo', N'TABLE', N'trn_address', N'COLUMN', N'zip_code';
EXECUTE sp_addextendedproperty N'MS_Description', N'都道府県名', N'SCHEMA', N'dbo', N'TABLE', N'trn_address', N'COLUMN', N'pref';
EXECUTE sp_addextendedproperty N'MS_Description', N'市区町村名', N'SCHEMA', N'dbo', N'TABLE', N'trn_address', N'COLUMN', N'city';
EXECUTE sp_addextendedproperty N'MS_Description', N'町域名', N'SCHEMA', N'dbo', N'TABLE', N'trn_address', N'COLUMN', N'town';
EXECUTE sp_addextendedproperty N'MS_Description', N'番地等', N'SCHEMA', N'dbo', N'TABLE', N'trn_address', N'COLUMN', N'block';
EXECUTE sp_addextendedproperty N'MS_Description', N'アパート等', N'SCHEMA', N'dbo', N'TABLE', N'trn_address', N'COLUMN', N'apart';
EXECUTE sp_addextendedproperty N'MS_Description', N'電話番号', N'SCHEMA', N'dbo', N'TABLE', N'trn_address', N'COLUMN', N'tel';
EXECUTE sp_addextendedproperty N'MS_Description', N'メール', N'SCHEMA', N'dbo', N'TABLE', N'trn_address', N'COLUMN', N'mail';
EXECUTE sp_addextendedproperty N'MS_Description', N'メモ', N'SCHEMA', N'dbo', N'TABLE', N'trn_address', N'COLUMN', N'memo';

EXECUTE sp_addextendedproperty N'MS_Description', N'住所マスタ', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', NULL, NULL;
EXECUTE sp_addextendedproperty N'MS_Description', N'ＩＤ', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'id';
EXECUTE sp_addextendedproperty N'MS_Description', N'全国地方公共団体コード', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'gov_code';
EXECUTE sp_addextendedproperty N'MS_Description', N'郵便番号（旧）', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'zip_code_old';
EXECUTE sp_addextendedproperty N'MS_Description', N'郵便番号', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'zip_code';
EXECUTE sp_addextendedproperty N'MS_Description', N'都道府県名（カナ）', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'pref_kana';
EXECUTE sp_addextendedproperty N'MS_Description', N'市区町村名（カナ）', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'city_cana';
EXECUTE sp_addextendedproperty N'MS_Description', N'町域名（カナ）', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'town_kana';
EXECUTE sp_addextendedproperty N'MS_Description', N'都道府県名', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'pref';
EXECUTE sp_addextendedproperty N'MS_Description', N'市区町村名', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'city';
EXECUTE sp_addextendedproperty N'MS_Description', N'町域名', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'town';
EXECUTE sp_addextendedproperty N'MS_Description', N'コード１', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'code1';
EXECUTE sp_addextendedproperty N'MS_Description', N'コード２', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'code2';
EXECUTE sp_addextendedproperty N'MS_Description', N'コード３', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'code3';
EXECUTE sp_addextendedproperty N'MS_Description', N'コード４', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'code4';
EXECUTE sp_addextendedproperty N'MS_Description', N'コード５', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'code5';
EXECUTE sp_addextendedproperty N'MS_Description', N'コード６', N'SCHEMA', N'dbo', N'TABLE', N'mst_address', N'COLUMN', N'code6';

