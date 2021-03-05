using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInput.SqlString
{
    class SalesInputSQLstring
    {
        public string SQLConnectionString
        {
            get
            {
                return "Data Source=192.168.0.219;Initial Catalog=FL00SS;User ID=sa;Password=WELLAN; timeout=10240;";
            }
        }
        public string FILASQLConnectionString
        {
            get
            {
                return "Data Source=192.168.0.217;Initial Catalog=FILA;User ID=sa;Password=WELLAN; timeout=10240;";
            }
        }
        public string PickupConnectionString
        {
            get
            {
                return @"Data Source=.\sqlexpress;Initial Catalog=PickupDB;Integrated Security=True;timeout=10240;";
            }
        }
        public string SalesOrderDescribe
        {
            get
            {
                return @"SELECT RBITM,櫃號,貨號,顏色,尺寸,BSTO.BSONU AS 條碼,FLOOR(BSEED) AS 定價,Replace(Convert(Varchar(12),CONVERT(money,BSEED),1),'.00','') AS 定價 ,數量,RBDCX AS 折數,FLOOR(RBUP1) AS 實售價值,Replace(Convert(Varchar(12),CONVERT(money,RBUP1),1),'.00','') AS 實售價  FROM(SELECT RANUM AS 櫃號,RBNCR AS 貨號,RBCLR AS 顏色,sz as 尺寸,數量,RBITM,BSEED,RBDCX,RBUP1
                        FROM (
                                SELECT distinct  RBITM,RAREN,RBNCR,RBCLR,RBCLZ,RANUM,bstl.BSUPC,bstl.BSEEP,bstl.BSEED,RBDCX,RBUP1,RBQTY01,RBQTY02,RBQTY03,RBQTY04,RBQTY05,RBQTY06,RBQTY07,RBQTY08,RBQTY09,RBQTY10,RBQTY11,RBQTY12,RBQTY13,RBQTY14,RBQTY15,RBQTY16,RBQTY17,RBQTY18,RBQTY19,RBQTY20
                                FROM RSICB
                              
                                JOIN BSTL ON RSICB.RBNCR=BSTL.BSNCR AND RSICB.RBCLR=BSTL.BSCLR
                                JOIN CodeB ON BSTL.BSSEA=CodeB.CodeID AND CodeB.CodeType='FMMSTSEA'
                                JOIN RSICA ON RSICB.RBREN=RSICA.RAREN
                                WHERE RSICA.RAREN=@A 
                        )AS P
                        UNPIVOT(
                                數量 FOR 測試 IN (RBQTY01,RBQTY02,RBQTY03,RBQTY04,RBQTY05,RBQTY06,RBQTY07,RBQTY08,RBQTY09,RBQTY10,RBQTY11,RBQTY12,RBQTY13,RBQTY14,RBQTY15,RBQTY16,RBQTY17,RBQTY18,RBQTY19,RBQTY20)
                        )AS PV
                        JOIN NewSIZE ON RBCLZ=sztp AND 測試=Category
                        WHERE 數量 != 0
                )as GroupTable 
                JOIN BSTO ON BSTO.BSNUM=貨號+顏色+尺寸 group by 櫃號,貨號,顏色,尺寸,BSTO.BSONU,BSEED,RBDCX,RBUP1,數量,RBITM ORDER BY RBITM DESC ";
            }
        }
        public string InputOrderDescribe
        {
            get
            {
                return @"SELECT RBITM,櫃號,貨號,顏色,尺寸,BSTO.BSONU AS 條碼,BSEED,數量 FROM(SELECT RANUM AS 櫃號,RBNCR AS 貨號,RBCLR AS 顏色,sz as 尺寸,數量,RBITM,BSEED
                        FROM (
                                SELECT distinct  RBITM,RAREN,RBNCR,RBCLR,RBCLZ,RANUM,bstl.BSUPC,bstl.BSEEP,bstl.BSEED,RBQTY01,RBQTY02,RBQTY03,RBQTY04,RBQTY05,RBQTY06,RBQTY07,RBQTY08,RBQTY09,RBQTY10,RBQTY11,RBQTY12,RBQTY13,RBQTY14,RBQTY15,RBQTY16,RBQTY17,RBQTY18,RBQTY19,RBQTY20
                                FROM RSIAB
                              
                                JOIN BSTL ON RSIAB.RBNCR=BSTL.BSNCR
                                JOIN CodeB ON BSTL.BSSEA=CodeB.CodeID AND CodeB.CodeType='FMMSTSEA'
                                JOIN RSIAA ON RSIAB.RBREN=RSIAA.RAREN
                                WHERE RSIAA.RAREN=@A 
                        )AS P
                        UNPIVOT(
                                數量 FOR 測試 IN (RBQTY01,RBQTY02,RBQTY03,RBQTY04,RBQTY05,RBQTY06,RBQTY07,RBQTY08,RBQTY09,RBQTY10,RBQTY11,RBQTY12,RBQTY13,RBQTY14,RBQTY15,RBQTY16,RBQTY17,RBQTY18,RBQTY19,RBQTY20)
                        )AS PV
                        JOIN NewSIZE ON RBCLZ=sztp AND 測試=Category
                        WHERE 數量 != 0
                )as GroupTable 
                JOIN BSTO ON BSTO.BSNUM=貨號+顏色+尺寸 group by 櫃號,貨號,顏色,尺寸,BSTO.BSONU,BSEED,數量,RBITM ORDER BY RBITM DESC  ";
            }
        }

        public string SalesOrderHeader
        {
            get
            {
                return @"SELECT RAKIN,RAREN AS '單號',RACNS AS '櫃點',A.BCNAM AS '櫃點名',RANUM AS '客戶',B.BCNAM AS '客戶名',convert(varchar, RADAY, 111) AS '日期',RARMA AS '備註',RAADR AS '地址',RAKIN AS '出貨類別',(select CodeName from CodeB where CodeType = 'FMICAKIN' AND CodeId = RAKIN) AS '類別名稱',RAMEN AS '員編',BPNME AS '姓名' ,Replace(Convert(Varchar(20),CONVERT(money,RATOL),1),'.00','') AS '合計金額',RAQTY   AS '合計數量' FROM RSICA, BCUS AS A, BCUS AS B, BPSN WHERE A.BCNUM = RACNS
                         AND B.BCNUM = RANUM AND BPENO = RAMEN  ";
            }
        }
        public string InputOrderHeader
        {
            get
            {
                return @"SELECT [RAREN] as '單號'
                          ,[RACNS] as '入庫櫃點'
                          ,(select BCNAM from BCUS where BCNUM=[RACNS])as '櫃點名稱'
                          ,[RANUM] as '客戶編號'
                           ,(select BCNAM from BCUS where BCNUM=[RANUM])as '客戶名稱'
                            ,[RAMEN] as '人員編號'
                           ,(select BPNME from BPSN where BPENO=[RAMEN])as '開單人員'
                          ,[RADAY] as '日期'
                          ,[RARMA] as '備註'
                          ,[RATAX] as '稅額'
                          ,[RATOL] as '合計金額'
                          ,[RAQTY] as '數量'
                          ,(SELECT CODENAME FROM CODEB WHERE CodeType='IVSCS1' AND CodeId= [RACS1])as '計稅方式'
						  ,(SELECT CODENAME FROM CODEB WHERE CodeType='IVSCS2' AND CodeId= [RACS2])as '稅率類別'						  
                      FROM [FL00SS].[dbo].[RSIAA]
                      where [RAREN]=[RAREN] ";
            }
        }

        public string InventoryOrderHeader
        {
            get
            {
                return @"SELECT * FROM RSIIA,BPSN  where RAMEN=BPENO ";
            }
        }

        public string StoreContact
        {
            get
            {
                return @"SELECT 
                         BCNUM AS 櫃點,
                         BCNAM AS 分倉名稱,
                         CityName+BCTWN+BCADR as 地址,
                         BCTEL AS 電話
                         FROM [FL00SS].[dbo].[BCUS]
                         join  [FL00SS].[dbo].[uAddressCity] on [CityCode]=BCCTY ";
            }
        }



        public string InventoryOrderDescribe
        {
            get
            {
                return @"SELECT RBITM,序號,貨號,顏色,尺寸,BSTO.BSONU AS 條碼,FLOOR(BSEED) AS 定價,Replace(Convert(Varchar(12),CONVERT(money,BSEED),1),'.00','') AS 定價 ,數量 FROM(SELECT RASER AS 序號,RBNCR AS 貨號,RBCLR AS 顏色,sz as 尺寸,數量,RBITM,BSEED
                        FROM (
                                SELECT distinct  RBITM,RAREN,RBNCR,RBCLR,RBCLZ,RSIIA.RASER,bstl.BSUPC,bstl.BSEEP,bstl.BSEED,RBQTY01,RBQTY02,RBQTY03,RBQTY04,RBQTY05,RBQTY06,RBQTY07,RBQTY08,RBQTY09,RBQTY10,RBQTY11,RBQTY12,RBQTY13,RBQTY14,RBQTY15,RBQTY16,RBQTY17,RBQTY18,RBQTY19,RBQTY20
                                FROM RSIIB
                              
                                JOIN BSTL  ON RSIIB.RBNCR=BSTL.BSNCR AND RSIIB.RBCLR=BSTL.BSCLR
                                JOIN CodeB ON BSTL.BSSEA=CodeB.CodeID AND CodeB.CodeType='FMMSTSEA'
                                JOIN RSIIA ON RSIIB.RBCNS=RSIIA.RACNS AND RSIIB.RBDAY=RSIIA.RADAY AND RSIIB.RBSER=RSIIA.RASER
                                WHERE RACNS=@a AND  RADAY=@b AND  RSIIA.RASER=@c
                        )AS P
                        UNPIVOT(
                                數量 FOR 測試 IN (RBQTY01,RBQTY02,RBQTY03,RBQTY04,RBQTY05,RBQTY06,RBQTY07,RBQTY08,RBQTY09,RBQTY10,RBQTY11,RBQTY12,RBQTY13,RBQTY14,RBQTY15,RBQTY16,RBQTY17,RBQTY18,RBQTY19,RBQTY20)
                        )AS PV
                        JOIN NewSIZE ON RBCLZ=sztp AND 測試=Category
                        WHERE 數量 != 0
                )as GroupTable 
                JOIN BSTO ON BSTO.BSNUM=貨號+顏色+尺寸 group by 序號,貨號,顏色,尺寸,BSTO.BSONU,BSEED,數量,RBITM ORDER BY RBITM DESC ";
            }
        }

        public string SQL_BarcodeString
        {
            get
            {
                return @"SELECT  BSTO.BSNCR AS 型號,BSTO.BSCLR AS 顏色,BSTO.BSSIZ AS 尺寸,BSTO.BSONU as 條碼,BSTN.BSNAM as 中文名稱,BSTL.BSONR as 英文名稱,BSTL.BSMAT as 材質一,BSTL.BSMA1 as 材質二,BSTL.BSMA2 as 材質三,BSTL.BSMA3 as 材質四,BSTL.BSMA4 as 材質五,BSTL.BSEED as 最初價,BSTL.BSEEP AS 零售價,CB.CodeName AS 產地 ,BCUS.BCNAM AS  製造商,BCUS.BCADR AS  地址,BCUS.BCTEL AS  電話,BSTN.BSMYM AS 製造年月  FROM BSTO JOIN BSTL ON BSTO.BSNCR=BSTL.BSNCR AND BSTO.BSCLR=BSTL.BSCLR JOIN BSTN ON BSTO.BSNCR=BSTN.BSNCR JOIN CodeB ON CodeType='FMMSTSEA' AND CodeB.CodeId=BSTL.BSSEA LEFT JOIN CodeB as CB ON CB.CodeType='FMMSTFAC' AND CB.CodeId=BSTN.BSFAC LEFT JOIN BCUS ON BSTN.BSCUS=BCUS.BCNUM 
                         WHERE 
                         BSTO.BSNCR like '%' ";
            }
        }

        public string SQL_ProductDetail
        {
            get
            {
                return @"select [BSTO].BSNCR,BSCLR,BSSIZ,BSCLS,category from [BSTO] join [BSTN] on [BSTO].BSNCR=[BSTN].BSNCR join  NewSIZE on BSCLS=sztp and BSSIZ=sz  where  BSONU=@A;";
            }
        }



        public string TransferOrderDescribe
        {
            get
            {
                return "SELECT RBITM,櫃號,貨號,顏色,尺寸,BSTO.BSONU AS 條碼,數量 FROM(SELECT RACN2 AS 櫃號,RBNCR AS 貨號,RBCLR AS 顏色,sz as 尺寸,數量,RBITM                  FROM (                    SELECT distinct  RBITM,RAREN,RBNCR,RBCLR,RBCLZ,RACN2,bstl.BSUPC,bstl.BSEEP,bstl.BSEED,RBQTY01,RBQTY02,RBQTY03,RBQTY04,RBQTY05,RBQTY06,RBQTY07,RBQTY08,RBQTY09,RBQTY10,RBQTY11,RBQTY12,RBQTY13,RBQTY14,RBQTY15,RBQTY16,RBQTY17,RBQTY18,RBQTY19,RBQTY20                    FROM RSIMB                JOIN BSTL ON RSIMB.RBNCR=BSTL.BSNCR                               JOIN CodeB ON BSTL.BSSEA=CodeB.CodeID AND CodeB.CodeType='FMMSTSEA'           JOIN RSIMA ON RSIMB.RBREN=RSIMA.RAREN                              WHERE RSIMA.RAREN=@a                      )AS P                      UNPIVOT(                             數量 FOR 測試 IN (RBQTY01,RBQTY02,RBQTY03,RBQTY04,RBQTY05,RBQTY06,RBQTY07,RBQTY08,RBQTY09,RBQTY10,RBQTY11,RBQTY12,RBQTY13,RBQTY14,RBQTY15,RBQTY16,RBQTY17,RBQTY18,RBQTY19,RBQTY20)                       )AS PV                   JOIN NewSIZE ON RBCLZ=sztp AND 測試=Category                        WHERE 數量 != 0   )as GroupTable               JOIN BSTO ON BSTO.BSNUM=貨號+顏色+尺寸 group by 櫃號,貨號,顏色,尺寸,BSTO.BSONU,數量,RBITM ORDER BY RBITM DESC ";
            }
        }

        public string TransferOrderHeader
        {
            get
            {
                return "SELECT * FROM RSIMA  WHERE RAREN LIKE ''  ";
            }
        }
        public string CheckVersions
        {
            get
            {
                return " select * from PickupSystem";
            }
        }

        public string PickupADetail
        {
            get
            {
                return @"SELECT [OrderID] as '單號'
                         ,[OrderStore]  as '櫃號'
                         ,[OrderName] as '櫃名'
                          ,[OrderAmount] as '數量'
                          ,[OrderSate] as '單據狀態'
                           ,[OrderUploade] as '下載狀態'
                             ,[OrderDate] as '日期'
                              ,[OrderStaff] as '異動員工'
                             FROM [PickupA] where OrderID like '%' ";
            }
        }

        public string PickupBDetail
        {
            get
            {
                return @"SELECT [OrderSN]
                      ,[OrderID]
                      ,[Order_Location]
                      ,[Order_Type]
                      ,[Order_Color]
                      ,[Order_Size]
                      ,[Order_Amount]
                      ,[Order_PickAmount]
                      ,[Order_Barcode]
                      ,[Order_BoxNum]
                      ,[Order_BoxState]
                      ,[Order_Store]
                      ,[Order_State]
                      ,[Order_Location2]
                      ,[Order_Stock]
                       FROM [PickupB]
                      where OrderID=@A";
            }
        }
        public string PickupUpdate
        {
            get
            {
                return @"  update [PickupA] set [OrderUploade]='Y',[OrderStaff]=@B  WHERE [OrderID]=@A;  ";
            }
        }
        public string PickupRemove
        {
            get
            {
                return @"  update [PickupA] set [OrderUploade]='N',[OrderStaff]=@B  WHERE [OrderID]=@A;  ";
            }
        }
        public string PickupFinish
        {
            get
            {
                return @"  update [PickupA] set [OrderDifference]=@C,[OrderSate]=@B,[OrderSpendTime]=@D  WHERE [OrderID]=@A;  ";
            }
        }

        public string PickupB_Update
        {
            get
            {
                return @"     update 
                              [PickupB]
                              set [Order_PickAmount]=@C
                              where [OrderID]=@A and [Order_Barcode]=@B  ";
            }
        }


        public string UserCheck
        {
            get
            {
                return @" SELECT [id]
      ,[account]
      ,[password]
      ,[name]
      ,[email]
      ,[groupType]
  FROM [user]
  where account=@a and password=@b ";
            }
        }


        public string NoticeA
        {
            get
            {
                return @"SELECT  *
                        FROM [FILA].[dbo].[POS_NoticeA]
                        where ID = ";
            }
        }


        public string NoticeADetail
        {
            get
            {
                return @"SELECT  id,date,Require,name,Status,Mapping,trade
                        FROM [FILA].[dbo].[POS_NoticeA]
                        where Response='3998' ";
            }
        }
        public string NoticeDetail_Print
        {
            get
            {
                return @"SELECT  TypeName,Color,Size,Sum(Amount)as Amount
                        FROM [FILA].[dbo].[POS_NoticeA]
                        JOIN [POS_NoticeB] ON [FILA].[dbo].[POS_NoticeA].ID=[FILA].[dbo].POS_NoticeB.AID
                        where Response='3998' ";
            }
        }
        public string NoticeBDetail
        {
            get
            {
                return @"SELECT   [TypeName]
                        ,[Color]
                        ,[Size]
                        ,[Amount],  [Shipments]
                        FROM [FILA].[dbo].[POS_NoticeB]
                        where AID = ";
            }
        }

        public string ShipInsert
        {
            get
            {
                return @"  INSERT INTO [FILA].[dbo].[POS_ShipOrder] (shipDate,shipID,shipTrace,shipERP,shipNotice,ShipReceiverName,ShipReceiverTel,ShipReceiverAddress,ShipImg,ShipState,ShipStation,ShipRemark,ShipPoint) VALUES(@a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k,@l,@m); ";
            }
        }

        public string ShipInsertTest
        {
            get
            {
                return @"  INSERT INTO [FILA].[dbo].[POS_ShipOrderTest] (shipDate,shipID,shipTrace,shipERP,shipNotice,ShipReceiverName,ShipReceiverTel,ShipReceiverAddress,ShipImg,ShipState,ShipStation,ShipRemark,ShipPoint) VALUES(@a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k,@l,@m); ";
            }
        }

        public string updateNoticeA
        {
            get
            {
                return @"update [FILA].[dbo].[POS_NoticeA] set [Status]=@A ,[Mapping] =@B,[Freight]=@C where [ID] =@D";
            }
        }
        public string updateNoticeB
        {
            get
            {
                return @"update[FILA].[dbo].[POS_NoticeB] SET Shipments = @a WHERE AID = @b and TypeName = @c and color = @d and size = @e";
            }
        }


        
    }
}
