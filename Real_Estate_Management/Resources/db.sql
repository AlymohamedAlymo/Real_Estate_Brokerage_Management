
GO
/****** Object:  UserDefinedFunction [dbo].[GetContractPayments]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE FUNCTION [dbo].[GetContractPayments](
@ContractNo as int ,  @RemainType as varchar(255) 
) RETURNS decimal (18,4)
AS
BEGIN
DECLARE @result as decimal (18,4)
SET @result = (SELECT  SUM(tbPayContract.amount) as remain  FROM tbPayContract
LEFT JOIN tbBillBody ON tbPayContract.contractno = tbBillBody.contractno
JOIN tbBillheader ON tbBillBody.parentguid = tbBillheader.guid
WHERE tbPayContract.paymenttype = 0 AND tbBillheader.billtype = 0 AND tbBillBody.contractno = @ContractNo  AND tbPayContract.AmountFor = ISNULL(@RemainType ,  tbPayContract.AmountFor)
GROUP BY  tbBillBody.contractno , (tbBillBody.TotalNet))

RETURN ISNULL(@result , 0)
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetContractRemain]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[GetContractRemain](
@ContractNo as int , @RemainType as varchar(255) 
) RETURNS decimal (18,4)
AS
BEGIN
DECLARE @result as decimal (18,4)
SET @result = (SELECT SUM(payin) - SUM(payout) as remain FROM (
SELECT CASE WHEN @RemainType  IS NULL THEN tbBillBody.totalnet WHEN @RemainType  = 'قيمة الأرض' THEN CAST(tbBillBody.price - tbBillBody.discounttotalvalue as decimal(18,4)) WHEN @RemainType = 'سعي عمولة الأرض' THEN tbBillBody.workfeevalue WHEN @RemainType = 'ضريبة عمولة السعي' THEN  tbBillBody.vatvalue END  as payin ,  0   as payout FROM tbBillBody 
JOIN tbBillheader ON tbBillBody.parentguid = tbBillheader.guid 
WHERE tbBillheader.billtype = 0 AND  tbBillBody.contractno = @ContractNo 
UNION ALL 
SELECT 0 as payin , amount  as payout FROM tbPayContract WHERE tbPayContract.paymenttype = 0 AND tbPayContract.contractno = @ContractNo AND tbPayContract.AmountFor = ISNULL(@RemainType , tbPayContract.AmountFor))
as contractremain )

RETURN ISNULL(@result , 0)
END
GO
/****** Object:  Table [dbo].[tbAccount]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbAccount](
	[guid] [uniqueidentifier] NULL,
	[number] [int] NULL,
	[name] [varchar](255) NULL,
	[note] [varchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbAgent]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbAgent](
	[guid] [uniqueidentifier] NULL,
	[number] [int] NULL,
	[name] [varchar](255) NULL,
	[civilid] [varchar](255) NULL,
	[mobile] [varchar](255) NULL,
	[email] [varchar](255) NULL,
	[vatid] [varchar](255) NULL,
	[publicnumber] [varchar](255) NULL,
	[agentname] [varchar](255) NULL,
	[agentcivilid] [varchar](255) NULL,
	[agentmobile] [varchar](255) NULL,
	[agentemail] [varchar](255) NULL,
	[agentvatid] [varchar](255) NULL,
	[agencynumber] [varchar](255) NULL,
	[agentpublicnumber] [varchar](255) NULL,
	[officename] [varchar](255) NULL,
	[officecr] [varchar](255) NULL,
	[officephone] [varchar](255) NULL,
	[officeemail] [varchar](255) NULL,
	[officevatid] [varchar](255) NULL,
	[officepublicnumber] [varchar](255) NULL,
	[note] [varchar](255) NULL,
	[agenttype] [int] NULL,
	[lastaction] [varchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbAppInfo]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbAppInfo](
	[Guid] [uniqueidentifier] NULL,
	[AppTitle] [varchar](255) NULL,
	[ColumnCount] [int] NULL,
	[BackupOnExit] [bit] NULL,
	[BackupPath] [varchar](255) NULL,
	[background] [image] NULL,
	[KMStyle] [int] NULL,
	[QtyFormat] [varchar](50) NULL,
	[CurrecnyFormat] [varchar](50) NULL,
	[Logo] [image] NULL,
	[ShowConfirmMSG] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbAccount]    Script Date: 8/5/2024 6:19:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbLog](
	[guid] [uniqueidentifier] NULL,
	[regdate] [date] NULL,
	[username] [varchar](255) NULL,
	[actiontype] [varchar](255) NULL,
	[action] [varchar](255) NULL,
	[note] [varchar](255) NULL
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[tbAttachment]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbAttachment](
	[Guid] [uniqueidentifier] NULL,
	[ParentGuid] [uniqueidentifier] NULL,
	[Name] [varchar](255) NULL,
	[FileName] [varchar](255) NULL,
	[FileSize] [varchar](255) NULL,
	[FileData] [image] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbBanks]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbBanks](
	[guid] [uniqueidentifier] NULL,
	[bankname] [varchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbBillBody]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbBillBody](
	[guid] [uniqueidentifier] NULL,
	[parentguid] [uniqueidentifier] NULL,
	[landguid] [uniqueidentifier] NULL,
	[number] [int] NULL,
	[contractno] [int] NULL,
	[price] [decimal](18, 4) NULL,
	[discounttotaltext] [varchar](255) NULL,
	[discounttotal] [decimal](18, 4) NULL,
	[discounttotalvalue] [decimal](18, 4) NULL,
	[buildingfeevalue] [decimal](18, 4) NULL,
	[workfeevalue] [decimal](18, 4) NULL,
	[vatvalue] [decimal](18, 4) NULL,
	[discountfeetext] [varchar](255) NULL,
	[discountfee] [decimal](18, 4) NULL,
	[discountfeevalue] [decimal](18, 4) NULL,
	[total] [decimal](18, 4) NULL,
	[totalnet] [decimal](18, 4) NULL,
	[note] [varchar](255) NULL,
	[status] [varchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbBillheader]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbBillheader](
	[guid] [uniqueidentifier] NULL,
	[ownerguid] [uniqueidentifier] NULL,
	[buyerguid] [uniqueidentifier] NULL,
	[billtype] [int] NULL,
	[number] [int] NULL,
	[regdate] [datetime] NULL,
	[ownerdata] [varchar](255) NULL,
	[buyerdata] [varchar](255) NULL,
	[total] [decimal](18, 4) NULL,
	[totaldiscounttotal] [decimal](18, 4) NULL,
	[totalbuidlingfee] [decimal](18, 4) NULL,
	[totalworkfee] [decimal](18, 4) NULL,
	[totalvat] [decimal](18, 4) NULL,
	[totaldiscountfee] [decimal](18, 4) NULL,
	[totalnet] [decimal](18, 4) NULL,
	[note] [varchar](255) NULL,
	[lastaction] [varchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbEditPassword]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbEditPassword](
	[guid] [uniqueidentifier] NULL,
	[password] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbEmailSettings]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbEmailSettings](
	[Guid] [uniqueidentifier] NULL,
	[SenderName] [varchar](50) NULL,
	[SenderEmail] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[Subject] [varchar](50) NULL,
	[MessageBody] [varchar](255) NULL,
	[SMTPServer] [varchar](50) NULL,
	[Port] [int] NULL,
	[UseSSL] [bit] NULL,
	[CCEmail] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbLand]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbLand](
	[PlanGuid] [uniqueidentifier] NULL,
	[guid] [uniqueidentifier] NULL,
	[code] [int] NULL,
	[number] [int] NULL,
	[blocknumber] [varchar](255) NULL,
	[landtype] [varchar](255) NULL,
	[area] [decimal](18, 4) NULL,
	[deednumber] [varchar](255) NULL,
	[amount] [decimal](18, 4) NULL,
	[isworkfee] [bit] NULL,
	[workfee] [decimal](18, 4) NULL,
	[issalefee] [bit] NULL,
	[salesfee] [decimal](18, 4) NULL,
	[isbuildingfee] [bit] NULL,
	[buildingfee] [decimal](18, 4) NULL,
	[isvat] [bit] NULL,
	[vat] [decimal](18, 4) NULL,
	[isdiscounttotal] [bit] NULL,
	[discounttotal] [decimal](18, 4) NULL,
	[discounttotalvalue] [decimal](18, 4) NULL,
	[isdiscountfee] [bit] NULL,
	[discountfee] [decimal](18, 4) NULL,
	[discountfeevalue] [decimal](18, 4) NULL,
	[total] [decimal](18, 4) NULL,
	[south] [varchar](255) NULL,
	[southdesc] [varchar](255) NULL,
	[north] [varchar](255) NULL,
	[northdesc] [varchar](255) NULL,
	[east] [varchar](255) NULL,
	[eastdesc] [varchar](255) NULL,
	[west] [varchar](255) NULL,
	[westdesc] [varchar](255) NULL,
	[status] [varchar](255) NULL,
	[reservereason] [varchar](255) NULL,
	[note] [varchar](255) NULL,
	[lastaction] [varchar](255) NULL

) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbLandTrans]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbLandTrans](
	[guid] [uniqueidentifier] NULL,
	[number] [int] NULL,
	[landguid] [uniqueidentifier] NULL,
	[regdate] [datetime] NULL,
	[deednumber] [varchar](255) NULL,
	[buildingnumber] [varchar](255) NULL,
	[note] [varchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbPay]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbPay](
	[guid] [uniqueidentifier] NULL,
	[accountguid] [uniqueidentifier] NULL,
	[paytype] [int] NULL,
	[number] [int] NULL,
	[paydate] [datetime] NULL,
	[amount] [decimal](18, 4) NULL,
	[note] [varchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbPayContract]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbPayContract](
	[guid] [uniqueidentifier] NULL,
	[paymenttype] [int] NULL,
	[number] [int] NULL,
	[paydate] [datetime] NULL,
	[refnumber] [int] NULL,
	[contractno] [int] NULL,
	[AmountFor] [varchar](255) NULL,
	[paytype] [varchar](255) NULL,
	[bank] [varchar](255) NULL,
	[checkno] [varchar](255) NULL,
	[amount] [decimal](18, 4) NULL,
	[note] [varchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbPaymentsNotes]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbPaymentsNotes](
	[guid] [uniqueidentifier] NULL,
	[landpricenotes] [varchar](1500) NULL,
	[workfeenotes] [varchar](1500) NULL,
	[vatnotes] [varchar](1500) NULL,
	[discountnotes] [varchar](1500) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbPicture]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbPicture](
	[guid] [uniqueidentifier] NULL,
	[ParentGuid] [uniqueidentifier] NULL,
	[Pic] [image] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbPlanInfo]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbPlanInfo](
	[guid] [uniqueidentifier] NULL,
	[name] [varchar](255) NULL,
	[ownerguid] [uniqueidentifier] NULL,
	[city] [varchar](255) NULL,
	[location] [varchar](255) NULL,
	[number] [varchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbPriceLog]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbPriceLog](
	[guid] [uniqueidentifier] NULL,
	[parentguid] [uniqueidentifier] NULL,
	[username] [varchar](255) NULL,
	[changedate] [datetime] NULL,
	[oldprice] [decimal](18, 4) NULL,
	[newprice] [decimal](18, 4) NULL,
	[actno] [int] NULL,
	[actdate] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbReportCustomView]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbReportCustomView](
	[guid] [uniqueidentifier] NULL,
	[name] [varchar](255) NULL,
	[ColumnView] [text] NULL,
	[AutoFillColumn] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbSaleOrder]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbSaleOrder](
	[guid] [uniqueidentifier] NULL,
	[regdate] [datetime] NULL,
	[number] [int] NULL,
	[buyerguid] [uniqueidentifier] NULL,
	[buyerdata] [varchar](255) NULL,
	[total] [decimal](18, 4) NULL,
	[totalnet] [decimal](18, 4) NULL,
	[note] [varchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbSaleOrderBody]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbSaleOrderBody](
	[guid] [uniqueidentifier] NULL,
	[parentguid] [uniqueidentifier] NULL,
	[number] [int] NULL,
	[landguid] [uniqueidentifier] NULL,
	[price] [decimal](18, 4) NULL,
	[total] [decimal](18, 4) NULL,
	[note] [varchar](255) NULL,
	[status] [varchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbSelectObject]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbSelectObject](
	[Guid] [uniqueidentifier] NULL,
	[Number] [int] NULL,
	[Name] [varchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbSMSsettings]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbSMSsettings](
	[guid] [uniqueidentifier] NULL,
	[Username] [varchar](255) NULL,
	[password] [varchar](255) NULL,
	[Sender] [varchar](255) NULL,
	[MessageBody] [varchar](255) NULL,
	[Mobile] [varchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbTaxDiscount]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbTaxDiscount](
	[guid] [uniqueidentifier] NULL,
	[isworkfee] [bit] NULL,
	[workfee] [decimal](18, 4) NULL,
	[issalefee] [bit] NULL,
	[salesfee] [decimal](18, 4) NULL,
	[isbuildingfee] [bit] NULL,
	[buildingfee] [decimal](18, 4) NULL,
	[isvat] [bit] NULL,
	[vat] [decimal](18, 4) NULL,
	[isdiscounttotal] [bit] NULL,
	[discounttotal] [decimal](18, 4) NULL,
	[discounttotalvalue] [decimal](18, 4) NULL,
	[isdiscountfee] [bit] NULL,
	[discountfee] [decimal](18, 4) NULL,
	[discountfeevalue] [decimal](18, 4) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbUsers]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbUsers](
	[guid] [uniqueidentifier] NULL,
	[name] [varchar](255) NULL,
	[password] [varchar](255) NULL,
	[IsAdmin] [bit] NULL,
	[CanAdd] [bit] NULL,
	[CanEdit] [bit] NULL,
	[CanDelete] [bit] NULL,
	[CanPrint] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbUsersPermissions]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbUsersPermissions](
	[Guid] [uniqueidentifier] NULL,
	[UserGuid] [uniqueidentifier] NULL,
	[PermissionName] [varchar](255) NULL,
	[PermissionValue] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbVatSettings]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbVatSettings](
	[guid] [uniqueidentifier] NULL,
	[companyname] [varchar](255) NULL,
	[vatid] [varchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[fnGetAgentData]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[fnGetAgentData] (@agenttype int , @datatype varchar(255) )
RETURNS TABLE
AS
RETURN
SELECT guid , number , agentname as name ,  agentcivilid as civilid, agentmobile as mobile , agentemail as email , agentvatid as vatid,  agencynumber as agencynumber ,  agentpublicnumber as  publicnumber from tbAgent WHERE agenttype = @agenttype AND  
@datatype  = 'الوكيل'
UNION ALL 
SELECT guid , number ,  name , civilid , mobile , email , vatid , '' as agencynumber ,   publicnumber from tbAgent WHERE agenttype = @agenttype AND  
@datatype  = 'المالك'
UNION ALL 
SELECT guid , number ,name , civilid , mobile , email , vatid , '' as agencynumber ,   publicnumber from tbAgent WHERE agenttype = @agenttype AND  
@datatype  = 'العميل'
UNION ALL 
SELECT guid , number , officename as name ,  officecr as civilid, officephone as mobile , officeemail as email , officevatid as vatid,  '' as agencynumber ,  officepublicnumber as  publicnumber from tbAgent WHERE agenttype = @agenttype AND  
@datatype  = 'المكتب'
GO
/****** Object:  View [dbo].[vwAgentData]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwAgentData] AS 
select * from [fnGetAgentData] (1 , 'الوكيل')
GO
/****** Object:  UserDefinedFunction [dbo].[fnPaysGrouped]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fnPaysGrouped] (@startdate datetime   , @enddate datetime )
RETURNS TABLE
AS
RETURN
SELECT MAX(Guid) as guid  , paytype  , SUM(Amount) as Amount FROM tbPayContract 
WHERE tbPayContract.paydate >= ISNULL(@startdate , tbPayContract.paydate)  AND  tbPayContract.paydate <= ISNULL(@enddate , tbPayContract.paydate) 
GROUP BY  paytype  


UNION ALL
SELECT MAX(Guid) as guid  , AmountFor  , SUM(Amount) as Amount FROM tbPayContract 
WHERE tbPayContract.paydate >= ISNULL(@startdate , tbPayContract.paydate) AND  tbPayContract.paydate <=  ISNULL(@enddate , tbPayContract.paydate) 
GROUP BY AmountFor 
GO
/****** Object:  View [dbo].[vwPaysGrouped]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwPaysGrouped] AS 
SELECT * FROM [fnPaysGrouped] (NULL , NULL )
GO
/****** Object:  UserDefinedFunction [dbo].[fnAccountBalance]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fnAccountBalance] (@startdate datetime   , @enddate datetime)
RETURNS TABLE
AS
RETURN
SELECT guid , number, name ,  SUM(amountin) as amountin ,  SUM(amountout) as amountout ,  SUM(amountin) -  SUM(amountout) as balance FROM ( 
SELECT tbAccount.guid , tbAccount.number ,  tbAccount.name ,  (amount) as amountin , 0 as amountout  FROM tbPay JOIN tbAccount ON tbAccount.guid = tbPay.accountguid 
WHERE paytype = 0 AND CAST(tbPay.PayDate AS DATE) >= ISNULL(@startdate , CAST(tbPay.PayDate AS DATE)) AND  CAST(  tbPay.PayDate AS DATE) <=  ISNULL(@enddate , CAST(tbPay.PayDate AS DATE))

UNION ALL 
SELECT tbAccount.guid , tbAccount.number ,  tbAccount.name ,  0 as amountin , (amount) as amountout  FROM tbPay JOIN tbAccount ON tbAccount.guid = tbPay.accountguid 
WHERE paytype = 1 AND CAST(tbPay.PayDate AS DATE) >= ISNULL(@startdate , CAST(tbPay.PayDate AS DATE)) AND  CAST(  tbPay.PayDate AS DATE) <=  ISNULL(@enddate , CAST(tbPay.PayDate AS DATE))
)  as accbalance 
GROUP BY guid , number , name 

GO
/****** Object:  UserDefinedFunction [dbo].[fnDailySalesGrouped]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fnDailySalesGrouped] (@startdate datetime   , @enddate datetime , @status varchar(255))
RETURNS TABLE
AS
RETURN
SELECT paydate , land ,  SUM(cash) as  cash, SUM(network) as network ,SUM(chick) as chick, SUM(bank) as bank,  SUM(cash) +  SUM(network) +  SUM(chick) +  SUM(bank) total FROM (
SELECT  CAST(  tbPayContract.PayDate AS DATE) as paydate  , 'مبيعات يوم ' + CAST(CAST(  tbPayContract.PayDate AS DATE) as varchar(255)) as land , (amount)   as cash  , cast( 0 as decimal (18,4)) as network ,  cast( 0 as decimal (18,4)) as chick ,    cast( 0 as decimal (18,4)) as bank , 0 as Total  , tbBillBody.status  FROM tbPayContract 
JOIN tbBillBody ON tbBillBody.contractno = tbPayContract.contractno 
WHERE (tbPayContract.paytype = 'نقدي' ) AND tbBillBody.status = @status AND tbPayContract.paymenttype = 0  AND  (CAST(tbPayContract.PayDate AS DATE) >= ISNULL(@startdate , CAST(tbPayContract.PayDate AS DATE)) AND  CAST(  tbPayContract.PayDate AS DATE) <=  ISNULL(@enddate , CAST(tbPayContract.PayDate AS DATE)))
UNION ALL
SELECT  CAST(  tbPayContract.PayDate AS DATE) as paydate  , 'مبيعات يوم ' + CAST(CAST(  tbPayContract.PayDate AS DATE) as varchar(255)) as land , cast( 0 as decimal (18,4))  as cash  ,  (amount)   as network  , cast( 0 as decimal (18,4)) as chick , cast( 0 as decimal (18,4)) as bank , 0 as Total  , tbBillBody.status  FROM tbPayContract 
JOIN tbBillBody ON tbBillBody.contractno = tbPayContract.contractno 
WHERE (tbPayContract.paytype = 'شبكة' ) AND tbBillBody.status = @status AND tbPayContract.paymenttype = 0  AND  (CAST(tbPayContract.PayDate AS DATE) >= ISNULL(@startdate , CAST(tbPayContract.PayDate AS DATE)) AND  CAST(  tbPayContract.PayDate AS DATE) <=  ISNULL(@enddate , CAST(tbPayContract.PayDate AS DATE)))
UNION ALL
SELECT  CAST(  tbPayContract.PayDate AS DATE) as paydate  , 'مبيعات يوم ' + CAST(CAST(  tbPayContract.PayDate AS DATE) as varchar(255)) as land ,  cast( 0 as decimal (18,4)) as cash  , (amount)   as network  , (amount)  as chick , cast( 0 as decimal (18,4)) as bank , 0 as Total  , tbBillBody.status  FROM tbPayContract 
JOIN tbBillBody ON tbBillBody.contractno = tbPayContract.contractno 
WHERE (tbPayContract.paytype = 'شيك' ) AND tbBillBody.status = @status AND tbPayContract.paymenttype = 0  AND  (CAST(tbPayContract.PayDate AS DATE) >= ISNULL(@startdate , CAST(tbPayContract.PayDate AS DATE)) AND  CAST(  tbPayContract.PayDate AS DATE) <=  ISNULL(@enddate , CAST(tbPayContract.PayDate AS DATE)))
UNION ALL
SELECT  CAST(  tbPayContract.PayDate AS DATE) as paydate  , 'مبيعات يوم ' + CAST(CAST(  tbPayContract.PayDate AS DATE) as varchar(255)) as land , cast( 0 as decimal (18,4))  as cash , cast( 0 as decimal (18,4)) as network ,cast( 0 as decimal (18,4)) as chick ,  (amount)    as bank , 0 as Total  , tbBillBody.status  FROM tbPayContract 
JOIN tbBillBody ON tbBillBody.contractno = tbPayContract.contractno 
WHERE (tbPayContract.paytype = 'تحويل بنكي' ) AND tbBillBody.status = @status AND tbPayContract.paymenttype = 0 AND  (CAST(tbPayContract.PayDate AS DATE) >= ISNULL(@startdate , CAST(tbPayContract.PayDate AS DATE)) AND  CAST(  tbPayContract.PayDate AS DATE) <=  ISNULL(@enddate , CAST(tbPayContract.PayDate AS DATE)))
 ) as dailysell
 GROUP BY CAST( PayDate AS DATE)   , land
GO
/****** Object:  View [dbo].[vwAccountBalance]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwAccountBalance] AS 
SELECT tbAccount.guid , tbAccount.number ,  tbAccount.name ,  0 as amountin , (amount) as amountout , 0 as balance  FROM tbPay JOIN tbAccount ON tbAccount.guid = tbPay.accountguid 
WHERE paytype = 1
GO
/****** Object:  View [dbo].[vwAccountStatement]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[vwAccountStatement] AS 
select tbPay.guid , tbPay.accountguid ,tbPay.paydate , tbPay.number ,tbAccount.name  , tbPay.amount as amountin , 0 as amountout, tbPay.note 

from tbPay
JOIN tbAccount ON tbAccount.guid = tbPay.accountguid 
 WHERE tbPay.paytype  = 0 
 UNION ALL 
select tbPay.guid , tbPay.accountguid ,tbPay.paydate , tbPay.number ,tbAccount.name  , 0  as amountin , tbPay.amount as amountout, tbPay.note 

from tbPay 
JOIN tbAccount ON tbAccount.guid = tbPay.accountguid 
WHERE tbPay.paytype  = 1 
GO
/****** Object:  View [dbo].[vwAddAmountToAgentStatement]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwAddAmountToAgentStatement] AS 
select tbBillheader.guid , tbBillheader.buyerguid ,tbBillheader.regdate , tbLand.code , 'رقم الأرض ' +  cast(tbLand.number as varchar(255)) as land , tbBillBody.contractno , CASE ISNULL(tbBillheader.buyerdata , '') WHEN 'الوكيل' THEN tbAgent.agentname  ELSE tbAgent.name END as agent  , tbBillBody.totalnet ,
[dbo].[GetContractPayments] ( tbBillBody.contractno , NULL ) as payments  , cast(0 as decimal(18,4)) as addpay  , [dbo].[GetContractRemain] ( tbBillBody.contractno , NULL ) as remain  ,  [dbo].[GetContractRemain] ( tbBillBody.contractno , NULL ) - [dbo].[GetContractPayments] ( tbBillBody.contractno , NULL )  as remainafterpay  
from tbBillBody
JOIN tbBillheader ON tbBillBody.parentguid = tbBillheader.guid
JOIN tbLand ON tbBillBody.landguid = tbLand.guid 
JOIN tbAgent ON tbAgent.guid = tbBillheader.buyerguid 

WHERE tbBillheader.billtype = 0 AND tbBillBody.status = 'مباع'
GO
/****** Object:  View [dbo].[vwAgentStatement]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwAgentStatement] AS 
select tbBillheader.guid , tbBillheader.buyerguid ,tbBillheader.regdate , tbLand.code , 'رقم الأرض ' +  cast(tbLand.number as varchar(255)) as land , tbBillBody.contractno , CASE ISNULL(tbBillheader.buyerdata , '') WHEN 'الوكيل' THEN tbAgent.agentname  ELSE tbAgent.name END as agent  , tbBillBody.totalnet ,
[dbo].[GetContractPayments] ( tbBillBody.contractno , NULL ) as payments   , [dbo].[GetContractRemain] ( tbBillBody.contractno , NULL ) as remain  
from tbBillBody
JOIN tbBillheader ON tbBillBody.parentguid = tbBillheader.guid
JOIN tbLand ON tbBillBody.landguid = tbLand.guid 
JOIN tbAgent ON tbAgent.guid = tbBillheader.buyerguid 

WHERE tbBillheader.billtype = 0 AND tbBillBody.status = 'مباع'
GO
/****** Object:  View [dbo].[vwAgentStatementDetails]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[vwAgentStatementDetails] AS 
SELECT * FROM (
select tbBillheader.guid , tbBillheader.buyerguid  , tbBillheader.number ,tbBillheader.regdate , tbLand.code , 'رقم الأرض ' +  cast(tbLand.number as varchar(255)) as land , tbBillBody.contractno , CASE ISNULL(tbBillheader.buyerdata , '') WHEN 'الوكيل' THEN tbAgent.agentname  ELSE tbAgent.name END as agent  , tbBillBody.totalnet as payin,
0 as payout    , tbBillBody.note 
from tbBillBody
JOIN tbBillheader ON tbBillBody.parentguid = tbBillheader.guid
JOIN tbLand ON tbBillBody.landguid = tbLand.guid 
JOIN tbAgent ON tbAgent.guid = tbBillheader.buyerguid 
WHERE tbBillheader.billtype = 0 AND tbBillBody.status = 'مباع'
UNION ALL
select tbPayContract.guid , tbBillheader.buyerguid,  tbPayContract.number , tbPayContract.paydate as regdate ,  tbLand.code , 'رقم الأرض ' +  cast(tbLand.number as varchar(255)) as land , tbBillBody.contractno , CASE ISNULL(tbBillheader.buyerdata , '') WHEN 'الوكيل' THEN tbAgent.agentname  ELSE tbAgent.name END as agent  , 0 as payin,
tbPayContract.amount as payout   , tbPayContract.note  
from tbPayContract
JOIN tbBillBody ON tbBillBody.contractno = tbPayContract.contractno
JOIN tbBillheader ON tbBillBody.parentguid = tbBillheader.guid
JOIN tbLand ON tbBillBody.landguid = tbLand.guid 
JOIN tbAgent ON tbAgent.guid = tbBillheader.buyerguid 
WHERE tbPayContract.paymenttype = 0 AND tbBillheader.billtype = 0 AND tbBillBody.status = 'مباع') as agentstatedetails

GO
/****** Object:  View [dbo].[vwBillBody]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE VIEW [dbo].[vwBillBody] AS 
SELECT tbBillBody.guid , tbBillBody.parentguid , tbBillBody.landguid ,  
tbbillbody.number,   contractno , tbland.code , 'الأرض رقم ' + CAST(tbLand.number as varchar(255))   as land , tbBillBody.price ,  tbBillBody.discounttotaltext,
tbBillBody.discounttotal ,  tbBillBody.discounttotalvalue ,  tbBillBody.buildingfeevalue ,  tbBillBody.workfeevalue ,  tbBillBody.vatvalue , tbBillBody.discountfeetext ,tbBillBody.discountfee , tbBillBody.discountfeevalue , (tbBillBody.price - tbBillBody.discounttotalvalue) as netprice ,  (tbBillBody.workfeevalue - tbBillBody.discountfeevalue) as networkfee , (tbBillBody.workfeevalue + tbBillBody.vatvalue) as vatworkfee ,   tbBillBody.total  , tbBillBody.totalnet  ,tbBillBody.note ,tbBillBody.status  
FROM tbBillBody
JOIN tbLand ON tbLand.guid = tbBillBody.landguid
GO
/****** Object:  View [dbo].[vwBuildingFee]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[vwBuildingFee] AS 
select tbBillheader.guid , tbBillheader.buyerguid ,tbBillheader.regdate , tbLand.code , tbLand.blocknumber , tbLand.number , 'رقم الأرض ' +  cast(tbLand.number as varchar(255)) as land , tbBillBody.price ,  tbBillBody.price - tbBillBody.discounttotalvalue as netprice, tbBillBody.buildingfeevalue ,  CASE ISNULL(tbBillheader.buyerdata , '') WHEN 'الوكيل' THEN tbAgent.agentname  ELSE tbAgent.name END as agent  ,  
 tbAgent.mobile , tbAgent.vatid 
from tbBillBody
JOIN tbBillheader ON tbBillBody.parentguid = tbBillheader.guid
JOIN tbLand ON tbBillBody.landguid = tbLand.guid 
JOIN tbAgent ON tbAgent.guid = tbBillheader.buyerguid 
WHERE tbBillheader.billtype = 0 AND tbBillBody.status = 'مباع'
GO
/****** Object:  View [dbo].[vwBuildingFeeNumbers]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE VIEW [dbo].[vwBuildingFeeNumbers] AS 
SELECT tbBillheader.guid , tbBillheader.buyerguid ,tbBillheader.regdate , tbBillBody.contractno , tbLand.blocknumber , tbLand.number , 'رقم الأرض ' +  cast(tbLand.number as varchar(255)) as land , tbBillBody.price ,  tbBillBody.price - tbBillBody.discounttotalvalue as netprice,  tbLandTrans.buildingnumber ,  tbBillBody.buildingfeevalue ,  CASE ISNULL(tbBillheader.buyerdata , '') WHEN 'الوكيل' THEN tbAgent.agentname  ELSE tbAgent.name END as agent  ,  
 tbAgent.mobile 
from tbBillBody
JOIN tbBillheader ON tbBillBody.parentguid = tbBillheader.guid
JOIN tbLand ON tbBillBody.landguid = tbLand.guid 
JOIN tbAgent ON tbAgent.guid = tbBillheader.buyerguid 
JOIN tbLandTrans ON tbLandTrans.landguid = tbBillBody.landguid
WHERE tbBillheader.billtype = 0 AND tbBillBody.status = 'مباع' AND tbLandTrans.buildingnumber <> '' 
GO
/****** Object:  View [dbo].[vwContractInfo]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vwContractInfo] AS 
select tbBillBody.guid ,  tbBillBody.landguid , tbBillheader.buyerguid  ,tbBillheader.regdate ,  tbBillBody.contractno  , tbLand.code ,  'الأرض رقم ' + CAST(tbLand.number as varchar(255))   as land , CASE ISNULL(tbBillheader.buyerdata , '') WHEN 'الوكيل' THEN tbAgent.agentname  ELSE tbAgent.name END as agent , CASE ISNULL(tbBillheader.buyerdata , '') WHEN 'الوكيل' THEN tbAgent.agentmobile  ELSE tbAgent.mobile END as mobile , CASE ISNULL(tbBillheader.buyerdata , '') WHEN 'الوكيل' THEN tbAgent.agentcivilid  ELSE tbAgent.civilid END  as civilid ,  tbBillBody.totalnet , dbo.GetContractRemain(tbBillBody.contractno , NULL ) as remain  , tbBillBody.price - tbBillBody.discountfeevalue as landprice, dbo.GetContractRemain(tbBillBody.contractno , 'قيمة الأرض' ) as landpriceremain ,  tbBillBody.workfeevalue  - tbBillBody.discountfeevalue as workfeevalue,  dbo.GetContractRemain(tbBillBody.contractno , 'سعي عمولة الأرض' ) as workfeevalueremain , tbBillBody.vatvalue ,  dbo.GetContractRemain(tbBillBody.contractno , 'ضريبة عمولة السعي' ) as vatvalueremain ,  tbBillBody.status , tbBillheader.billtype  from tbBillBody
JOIN tbLand ON tbLand.guid = tbBillBody.landguid 
JOIN tbBillheader ON tbBillheader.guid = tbBillBody.parentguid 
JOIN tbAgent on tbBillheader.buyerguid = tbAgent.guid
GO
/****** Object:  View [dbo].[vwcontractRemain]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwcontractRemain] AS 
SELECT tbBillBody.contractno ,   ISNULL(tbBillBody.totalnet  , 0 )  - SUM(amount) as remain FROM tbPaycontractbody  
LEFT JOIN tbBillBody ON tbPaycontractbody.contractno = tbBillBody.contractno 
GROUP BY tbBillBody.contractno , tbBillBody.totalnet
GO
/****** Object:  View [dbo].[vwContractRpt]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwContractRpt] AS 
SELECT tbBillheader.guid ,  tbBillBody.landguid , tbBillheader.buyerguid  ,tbBillheader.regdate ,  tbBillBody.contractno  , tbLand.code ,  'الأرض رقم ' + CAST(tbLand.number as varchar(255))   as land , CASE ISNULL(tbBillheader.buyerdata , '') WHEN 'الوكيل' THEN tbAgent.agentname  ELSE tbAgent.name END as agent , CASE ISNULL(tbBillheader.buyerdata , '') WHEN 'الوكيل' THEN tbAgent.agentmobile  ELSE tbAgent.mobile END as mobile , CASE ISNULL(tbBillheader.buyerdata , '') WHEN 'الوكيل' THEN tbAgent.agentcivilid  ELSE tbAgent.civilid END  as civilid ,  tbBillBody.totalnet , dbo.GetContractRemain(tbBillBody.contractno , NULL ) as remain  , tbBillBody.price - tbBillBody.discountfeevalue as landprice, dbo.GetContractRemain(tbBillBody.contractno , 'قيمة الأرض' ) as landpriceremain ,  tbBillBody.workfeevalue  - tbBillBody.discountfeevalue as workfeevalue,  dbo.GetContractRemain(tbBillBody.contractno , 'سعي عمولة الأرض' ) as workfeevalueremain , tbBillBody.vatvalue ,  dbo.GetContractRemain(tbBillBody.contractno , 'ضريبة عمولة السعي' ) as vatvalueremain ,  tbBillBody.status , tbBillheader.billtype  from tbBillBody
JOIN tbLand ON tbLand.guid = tbBillBody.landguid 
JOIN tbBillheader ON tbBillheader.guid = tbBillBody.parentguid 
JOIN tbAgent on tbBillheader.buyerguid = tbAgent.guid WHERE billtype = 0 
GO
/****** Object:  View [dbo].[vwDailSells]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwDailSells] AS 
SELECT tbBillheader.guid , ISNULL(tbPayContract.Guid , CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER)) as payguid  ,  tbBillheader.regdate ,    tbLand.code , 'الأرض رقم ' +  CAST(tbLand.number as varchar(255)) as land  , tbBillBody.contractno  ,
ISNULL(CASE WHEN tbPayContract.paytype IN( 'نقدي' ) THEN tbPayContract.amount ELSE 0 END , 0) - ISNULL(CASE WHEN tbPayContract.AmountFor IN( 'خصم إستثنائي' ) THEN tbPayContract.amount ELSE 0 END , 0) as cash   ,
ISNULL(CASE WHEN tbPayContract.paytype IN(  'شبكة'  ) THEN tbPayContract.amount ELSE 0 END , 0) as network   ,
ISNULL(CASE WHEN tbPayContract.paytype IN( 'شيك'  ) THEN tbPayContract.amount ELSE 0 END , 0) as chick   ,
ISNULL(CASE WHEN tbPayContract.paytype IN(  'تحويل بنكي' ) THEN tbPayContract.amount ELSE 0 END , 0) as bank   ,
ISNULL(tbPayContract.bank , '') as bankname    , ISNULL(tbPayContract.checkno , '') as checkno ,  ISNULL(tbPayContract.Number , 0)   as paynumber  ,ISNULL(tbPayContract.refNumber , 0)   as payrefnumber  , CASE WHEN paydate IS NULL THEN '' ELSE  CAST(Convert(date, paydate   ) as varchar(255)) END as paydate , tbBillBody.status 
FROM tbBillBody 
JOIN tbBillheader ON tbBillBody.parentguid = tbBillheader.guid
JOIN tbLand ON tbLand.guid = tbBillBody.landguid 
LEFT JOIN tbPayContract ON tbBillBody.contractno = tbPayContract.ContractNo 
WHERE tbPayContract.paymenttype = 0 and tbBillheader.billtype = 0
GO
/****** Object:  View [dbo].[vwDailySalesGrouped]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vwDailySalesGrouped] AS 
SELECT CAST(  tbPayContract.PayDate AS DATE)  as PayDate , CASE WHEN tbPayContract.paytype IN( 'نقدي' , 'شبكة' ) THEN  SUM(amount) ELSE 0 END  as cash  , CASE WHEN tbPayContract.paytype IN( 'نقدي' , 'شبكة' ) THEN   0 ELSE SUM(amount) END  as bank , SUM(Amount) as Total  , tbbillbody.status as x FROM tbPayContract 
JOIN tbBillBody ON tbBillBody.contractno = tbPayContract.contractno
WHERE tbPayContract.paymenttype = 0 
GROUP BY CAST(  tbPayContract.PayDate AS DATE)  , PayType  , tbBillBody.status 
GO
/****** Object:  View [dbo].[vwGeneralAccountInfo]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vwGeneralAccountInfo] AS 
SELECT SUM(totalsales) as totalsales , SUM(payments) as payments , SUM(remain) as remain  FROM (
select ISNULL(SUM(tbBillBody.totalnet) , 0) as totalsales  , 0 as payments , 0 as remain from tbBillBody
JOIN tbBillheader ON tbBillheader.guid = tbBillBody.parentguid 
WHERE billtype = 0 AND tbBillBody.status = 'مباع'
UNION ALL 
SELECT 0 as totalsales , ISNULL(SUM(tbPayContract.amount) , 0) as payments , 0 as remain FROM tbPayContract JOIN tbBillBody ON tbBillBody.contractno = tbPayContract.contractno 
JOIN tbBillheader ON tbBillheader.guid = tbBillBody.parentguid 
WHERE billtype = 0  AND tbPayContract.paymenttype = 0 AND tbBillBody.status = 'مباع'
UNION ALL 
SELECT 0 as totalsales ,  0 as payments ,  ISNULL( (tbBillBody.TotalNet) , 0) -  ISNULL(SUM(tbPayContract.amount), 0) as remain  FROM tbPayContract
LEFT JOIN tbBillBody ON tbPayContract.contractno = tbBillBody.contractno
JOIN tbBillheader ON tbBillheader.guid = tbBillBody.parentguid 
WHERE billtype = 0 AND tbPayContract.paymenttype = 0 AND tbBillBody.status = 'مباع' 
GROUP BY tbBillBody.totalnet 
) as info 
GO
/****** Object:  View [dbo].[vwGeneralAccountLandSales]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwGeneralAccountLandSales] AS 
SELECT max(guid) as guid , count(*) as c , title , paytype , SUM(totalnet) as totalnet , SUM(payments) as  payments , SUM(remain) as remain FROM (
select tbBillheader.guid ,  'مباعة نقدا و مسددة بالكامل'  as title , 'الكل'  as paytype , tbBillBody.totalnet , [dbo].[GetContractPayments] ( tbBillBody.contractno,NULL ) as payments   , [dbo].[GetContractRemain] ( tbBillBody.contractno,NULL ) as remain  
from tbBillBody
JOIN tbBillheader ON tbBillBody.parentguid = tbBillheader.guid 
WHERE tbBillheader.billtype = 0 AND [dbo].[GetContractRemain] ( tbBillBody.contractno,NULL ) = 0 AND 
tbBillBody.status = 'مباع'
UNION ALL
select tbBillheader.guid ,  'مباعة نقدا و لم تسدد بالكامل'  as title ,  'الكل'  as paytype , tbBillBody.totalnet , [dbo].[GetContractPayments] ( tbBillBody.contractno,NULL ) as payments   , [dbo].[GetContractRemain] ( tbBillBody.contractno,NULL ) as remain  
from tbBillBody
JOIN tbBillheader ON tbBillBody.parentguid = tbBillheader.guid 
WHERE tbBillheader.billtype = 0 AND [dbo].[GetContractRemain] ( tbBillBody.contractno ,NULL ) <> 0 AND 
tbBillBody.status = 'مباع'
UNION ALL
select tbBillheader.guid ,  'مسددة بالكامل'  as title , 'قيمة الأرض'  as paytype ,  (tbBillBody.price  - tbBillBody.discounttotalvalue) as totalnet  , [dbo].[GetContractPayments] ( tbBillBody.contractno, 'قيمة الأرض' ) as payments   , [dbo].[GetContractRemain] ( tbBillBody.contractno,'قيمة الأرض' ) as remain  
from tbBillBody
JOIN tbBillheader ON tbBillBody.parentguid = tbBillheader.guid 
WHERE tbBillheader.billtype = 0 AND [dbo].[GetContractRemain] ( tbBillBody.contractno, 'قيمة الأرض' ) = 0 AND 
tbBillBody.status = 'مباع'
UNION ALL
select tbBillheader.guid ,  'مسددة بالكامل'  as title , 'سعي عمولة الأرض'  as paytype ,  (tbBillBody.workfeevalue  - tbBillBody.discountfeevalue) as totalnet  , [dbo].[GetContractPayments] ( tbBillBody.contractno, 'سعي عمولة الأرض' ) as payments   , [dbo].[GetContractRemain] ( tbBillBody.contractno,'سعي عمولة الأرض' ) as remain  
from tbBillBody
JOIN tbBillheader ON tbBillBody.parentguid = tbBillheader.guid 
WHERE tbBillheader.billtype = 0 AND [dbo].[GetContractRemain] ( tbBillBody.contractno,'سعي عمولة الأرض' ) = 0 AND 
tbBillBody.status = 'مباع'
UNION ALL
select tbBillheader.guid ,  'مسددة بالكامل'  as title , 'ضريبة عمولة السعي'  as paytype ,  (tbBillBody.vatvalue) as totalnet  , [dbo].[GetContractPayments] ( tbBillBody.contractno, 'ضريبة عمولة السعي'   ) as payments   , [dbo].[GetContractRemain] ( tbBillBody.contractno, 'ضريبة عمولة السعي'   ) as remain  
from tbBillBody
JOIN tbBillheader ON tbBillBody.parentguid = tbBillheader.guid 
WHERE tbBillheader.billtype = 0 AND [dbo].[GetContractRemain] ( tbBillBody.contractno,'ضريبة عمولة السعي' ) = 0 AND 
tbBillBody.status = 'مباع'

UNION ALL
select tbBillheader.guid ,  'لم تسدد بالكامل'  as title , 'قيمة الأرض'  as paytype ,  (tbBillBody.price  - tbBillBody.discounttotalvalue) as totalnet  , [dbo].[GetContractPayments] ( tbBillBody.contractno, 'قيمة الأرض' ) as payments   , [dbo].[GetContractRemain] ( tbBillBody.contractno,'قيمة الأرض' ) as remain  
from tbBillBody
JOIN tbBillheader ON tbBillBody.parentguid = tbBillheader.guid 
WHERE tbBillheader.billtype = 0 AND [dbo].[GetContractRemain] ( tbBillBody.contractno, 'قيمة الأرض' ) <> 0 AND 
tbBillBody.status = 'مباع'
UNION ALL
select tbBillheader.guid ,  'لم تسدد بالكامل'  as title , 'سعي عمولة الأرض'  as paytype ,  (tbBillBody.workfeevalue  - tbBillBody.discountfeevalue) as totalnet  , [dbo].[GetContractPayments] ( tbBillBody.contractno, 'سعي عمولة الأرض' ) as payments   , [dbo].[GetContractRemain] ( tbBillBody.contractno,'سعي عمولة الأرض' ) as remain  
from tbBillBody
JOIN tbBillheader ON tbBillBody.parentguid = tbBillheader.guid 
WHERE tbBillheader.billtype = 0 AND [dbo].[GetContractRemain] ( tbBillBody.contractno,'سعي عمولة الأرض' ) <> 0 AND 
tbBillBody.status = 'مباع'
UNION ALL
select tbBillheader.guid ,  'لم تسدد بالكامل'  as title , 'ضريبة عمولة السعي'  as paytype ,  (tbBillBody.vatvalue) as totalnet  , [dbo].[GetContractPayments] ( tbBillBody.contractno, 'ضريبة عمولة السعي'   ) as payments   , [dbo].[GetContractRemain] ( tbBillBody.contractno, 'ضريبة عمولة السعي'   ) as remain  
from tbBillBody
JOIN tbBillheader ON tbBillBody.parentguid = tbBillheader.guid 
WHERE tbBillheader.billtype = 0 AND [dbo].[GetContractRemain] ( tbBillBody.contractno,'ضريبة عمولة السعي' ) <> 0 AND 
tbBillBody.status = 'مباع'

 ) as x 
GROUP BY title  , paytype
GO
/****** Object:  View [dbo].[vwGeneralSales]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwGeneralSales] AS 
select tbBillheader.guid , tbbillbody.contractno ,  tbBillheader.buyerguid ,tbBillheader.regdate , tbLand.code , tbLand.blocknumber , tbLand.number , 'رقم الأرض ' +  cast(tbLand.number as varchar(255)) as land ,  tbBillBody.price as price , tbBillBody.price - tbBillBody.discounttotalvalue as netprice , tbbillbody.buildingfeevalue ,  tbBillBody.workfeevalue ,  tbBillBody.workfeevalue - tbBillBody.discountfeevalue as networkfeevalue ,   tbBillBody.vatvalue , tbBillBody.workfeevalue - tbBillBody.discountfeevalue + tbBillBody.vatvalue as networkfeevaluewithvat , tbBillBody.totalnet ,  dbo.GetContractPayments(tbBillBody.contractno , NULL ) as Payments ,  dbo.[GetContractRemain](tbBillBody.contractno, NULL ) as remain ,  CASE ISNULL(tbBillheader.buyerdata , '') WHEN 'الوكيل' THEN tbAgent.agentname  ELSE tbAgent.name END as agent  ,  
 tbAgent.mobile , tbAgent.vatid 
from tbBillBody
JOIN tbBillheader ON tbBillBody.parentguid = tbBillheader.guid
JOIN tbLand ON tbBillBody.landguid = tbLand.guid 
JOIN tbAgent ON tbAgent.guid = tbBillheader.buyerguid 
WHERE tbBillheader.billtype = 0 AND tbBillBody.status = 'مباع'
GO
/****** Object:  View [dbo].[vwGetBillBodyStatus]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwGetBillBodyStatus] AS 
select tbBillBody.guid ,tbBillBody.landguid , tbBillBody.parentguid , tbBillheader.billtype , tbBillBody.contractno ,   tbBillBody.status  from tbBillBody
JOIN tbBillheader ON tbBillheader.guid = tbBillBody.parentguid 
GO
/****** Object:  View [dbo].[vwLand]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



 CREATE VIEW [dbo].[vwLand] AS 
 SELECT * FROM tbLand
GO
/****** Object:  View [dbo].[vwLandMainFiller]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwLandMainFiller] AS 
select * from tbland
GO
/****** Object:  View [dbo].[vwLandQty]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


 CREATE VIEW [dbo].[vwLandQty] AS 
 SELECT Guid , code , blocknumber ,  'رقم الأرض ' +  cast(tbLand.number as varchar(255)) as land , tbland.total ,  tbLand.workfee , ROUND(( tbland.total *   tbLand.workfee   /100 ) , 2) as workfeevalue ,   CASE [status] WHEN 'مباع' THEN  0 ELSE 1 END as qty , [status] FROM tbLand
GO
/****** Object:  View [dbo].[vwLandSales]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwLandSales] AS 
select tbBillheader.guid , tbBillheader.buyerguid ,tbBillheader.regdate , tbLand.code , tbLand.blocknumber , tbLand.number , 'رقم الأرض ' +  cast(tbLand.number as varchar(255)) as land ,  tbBillBody.price ,  tbBillBody.price  - tbBillBody.discounttotalvalue as netprice , tbBillBody.workfeevalue , [dbo].[GetContractPayments] ( tbBillBody.contractno , NULL ) as payments   , [dbo].[GetContractRemain] ( tbBillBody.contractno, NULL) as remain  , CASE ISNULL(tbBillheader.buyerdata , '') WHEN 'الوكيل' THEN tbAgent.agentname  ELSE tbAgent.name END as agent  ,  
 tbAgent.mobile  
from tbBillBody
JOIN tbBillheader ON tbBillBody.parentguid = tbBillheader.guid
JOIN tbLand ON tbBillBody.landguid = tbLand.guid 
JOIN tbAgent ON tbAgent.guid = tbBillheader.buyerguid 
WHERE tbBillBody.status = 'مباع' AND tbBillheader.billtype = 0 
GO
/****** Object:  View [dbo].[vwLandsType]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwLandsType] AS 
select MAX(guid) as guid ,  landtype  ,  count(*) as c     from tbLand
GROUP BY landtype 
GO
/****** Object:  View [dbo].[vwLandTrans]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwLandTrans] AS 
SELECT ISNULL(tbLandTrans.guid , 0x0) as guid ,  tbLand.guid as landguid , 0 as number , 'رقم الأرض ' +  cast(tbLand.number as varchar(255)) as land ,  CASE ISNULL(tbBillheader.buyerdata , '') WHEN 'الوكيل' THEN tbAgent.agentname  ELSE tbAgent.name END as agent ,  
CASE tbLandTrans.regdate  WHEN '1900-01-01' THEN NULL ELSE  tbLandTrans.regdate END as regdate, ISNULL(tbLandTrans.deednumber , '') as deednumber ,  
ISNULL(tbLandTrans.buildingnumber , '') as buildingnumber ,
tbBillBody.buildingfeevalue , 
[dbo].[GetContractRemain] ( tbBillBody.contractno , NULL ) as remain ,
ISNULL(tbLandTrans.note  , '') as note 
from tbBillBody
LEFT JOIN tbLandTrans ON tbBillBody.landguid = tbLandTrans.landguid
JOIN tbBillheader ON tbBillBody.parentguid = tbBillheader.guid
JOIN tbLand ON tbBillBody.landguid = tbLand.guid 
JOIN tbAgent ON tbAgent.guid = tbBillheader.buyerguid 
WHERE tbBillBody.status = 'مباع' AND tbBillheader.billtype = 0 
GO
/****** Object:  View [dbo].[vwOfficeRptFull]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwOfficeRptFull] AS 
select tbBillheader.guid ,   tbBillheader.buyerguid ,tbBillheader.regdate , tbbillbody.contractno ,  tbLand.code ,  tbLand.number , 'رقم الأرض ' +  cast(tbLand.number as varchar(255)) as land ,  tbBillBody.price as price , tbBillBody.discounttotalvalue,  tbBillBody.price - tbBillBody.discounttotalvalue as netprice ,   tbBillBody.workfeevalue ,  tbBillBody.discountfeevalue  ,  tbBillBody.workfeevalue - tbBillBody.discountfeevalue as networkfeevalue ,  dbo.GetContractPayments(tbBillBody.contractno , NULL ) as Payments ,  dbo.[GetContractRemain](tbBillBody.contractno, NULL ) as remain ,     dbo.GetContractPayments(tbBillBody.contractno , 'قيمة الأرض' ) as landPayments ,  dbo.[GetContractRemain](tbBillBody.contractno,  'قيمة الأرض' ) as landremain , dbo.GetContractPayments(tbBillBody.contractno , 'سعي عمولة الأرض' ) as feePayments ,  dbo.[GetContractRemain](tbBillBody.contractno,  'سعي عمولة الأرض' ) as feeremain   , dbo.GetContractPayments(tbBillBody.contractno , 'ضريبة عمولة السعي' ) as vatPayments ,  dbo.[GetContractRemain](tbBillBody.contractno,  'ضريبة عمولة السعي' ) as vatremain ,        tbBillBody.vatvalue ,    CASE ISNULL(tbBillheader.buyerdata , '') WHEN 'الوكيل' THEN tbAgent.agentname  ELSE tbAgent.name END as agent  ,  
 tbAgent.mobile  
from tbBillBody
JOIN tbBillheader ON tbBillBody.parentguid = tbBillheader.guid
JOIN tbLand ON tbBillBody.landguid = tbLand.guid 
JOIN tbAgent ON tbAgent.guid = tbBillheader.buyerguid 
WHERE tbBillheader.billtype = 0 AND tbBillBody.status = 'مباع'
GO
/****** Object:  View [dbo].[vwPayRpt]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwPayRpt] AS 
select tbPayContract.Guid , tbPayContract.number , tbPayContract.RefNumber,  CASE tbPayContract.paymenttype WHEN 0 THEN 'سند قبض' WHEN 2 THEN 'سند صرف' END as paymenttype , paydate  , tbPayContract.contractno , 'رقم الأرض ' +  cast(tbLand.number as varchar(255)) as land ,  CASE ISNULL(tbBillheader.buyerdata , '') WHEN 'الوكيل' THEN tbAgent.agentname  ELSE tbAgent.name END as agent , tbPayContract.AmountFor , tbPayContract.paytype , tbPayContract.bank , tbPayContract.checkno , tbPayContract.amount , tbPayContract.note , tbBillBody.status  from tbPayContract
JOIN tbBillBody ON tbBillBody.contractno = tbPayContract.contractno
JOIN tbBillheader ON tbBillBody.parentguid = tbBillheader.guid
JOIN tbAgent ON tbAgent.guid = tbBillheader.buyerguid 
JOIN tbLand ON tbBillBody.landguid = tbLand.guid 
WHERE  tbBillheader.billtype = 0
GO
/****** Object:  View [dbo].[vwSaleOrderBody]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwSaleOrderBody] AS 
select tbSaleOrderBody.guid , tbSaleOrderBody.parentguid , tbSaleOrderBody.landguid , tbSaleOrderBody.number ,  tbland.code , 'الأرض رقم ' + CAST(tbLand.number as varchar(255))   as land , tbSaleOrderBody.price ,   tbSaleOrderBody.total , tbSaleOrderBody.note , tbSaleOrderBody.status     from tbSaleOrderBody 
JOIN tbLand ON tbSaleOrderBody.landguid = tbLand.guid

GO
/****** Object:  View [dbo].[vwSalesOrderRpt]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwSalesOrderRpt] AS 
select tbSaleOrder.guid  , tbSaleOrder.buyerguid , tbSaleOrder.number , tbSaleOrder.regdate , tbLand.code , tbLand.blocknumber , tbLand.number as landnumber , 'رقم الأرض ' +  cast(tbLand.number as varchar(255)) as land ,     tbSaleOrderBody.price ,  tbSaleOrderBody.total  ,  CASE ISNULL(tbSaleOrder.buyerdata , '') WHEN 'الوكيل' THEN tbAgent.agentname  ELSE tbAgent.name END as agent   , tbSaleOrderBody.note from tbSaleOrder 
JOIN tbAgent ON tbSaleOrder.buyerguid = tbAgent.guid 

JOIN tbSaleOrderBody ON tbSaleOrderBody.parentguid = tbSaleOrder.guid 
JOIN tbLand ON tbLand.guid = tbSaleOrderBody.landguid 
GO
/****** Object:  View [dbo].[vwSelectLand]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[vwSelectLand] AS 
SELECT guid ,  code ,  number , blocknumber,  'الأرض رقم ' +  CAST(tbLand.number as varchar(255)) as land    , landtype , deednumber  , total  , status FROM tbLand
GO
/****** Object:  View [dbo].[vwStatic]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwStatic] AS 
select MAX(Guid) as guid , blocknumber , status ,  count (*) as c from tbLand
GROUP BY blocknumber ,  status

UNION ALL
select MAX(Guid) as guid , 'الكل' as blocknumber , status ,  count (*) as c from tbLand
GROUP BY   status

GO
/****** Object:  View [dbo].[vwWorkFee]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vwWorkFee] AS 
select tbBillheader.guid , tbBillheader.buyerguid ,tbBillheader.regdate , tbLand.code , tbLand.blocknumber , tbLand.number , 'رقم الأرض ' +  cast(tbLand.number as varchar(255)) as land ,  tbBillBody.price  - tbBillBody.discounttotalvalue as price , tbBillBody.workfeevalue - tbBillBody.discountfeevalue as workfeevalue ,  dbo.GetContractPayments(tbBillBody.contractno , NULL ) as Payments ,  dbo.[GetContractRemain](tbBillBody.contractno, NULL ) as remain ,  CASE ISNULL(tbBillheader.buyerdata , '') WHEN 'الوكيل' THEN tbAgent.agentname  ELSE tbAgent.name END as agent  ,  
 tbAgent.mobile , tbAgent.vatid 
from tbBillBody
JOIN tbBillheader ON tbBillBody.parentguid = tbBillheader.guid
JOIN tbLand ON tbBillBody.landguid = tbLand.guid 
JOIN tbAgent ON tbAgent.guid = tbBillheader.buyerguid 
WHERE tbBillBody.status = 'مباع' AND tbBillheader.billtype = 0 

GO
/****** Object:  View [dbo].[vwWorkFeeTotals]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwWorkFeeTotals] AS 
select tbBillheader.guid , tbBillheader.buyerguid ,tbBillheader.regdate , tbLand.code , tbLand.blocknumber , tbLand.number , 'رقم الأرض ' +  cast(tbLand.number as varchar(255)) as land ,  tbBillBody.price - tbBillBody.discounttotalvalue as price , tbBillBody.workfeevalue- tbBillBody.discountfeevalue as workfeevalue ,tbBillBody.vatvalue  ,  tbBillBody.workfeevalue  - tbBillBody.discountfeevalue +  tbBillBody.vatvalue as totalvat ,  dbo.GetContractPayments(tbBillBody.contractno, NULL ) as Payments ,  dbo.[GetContractRemain](tbBillBody.contractno , NULL ) as remain ,  CASE ISNULL(tbBillheader.buyerdata , '') WHEN 'الوكيل' THEN tbAgent.agentname  ELSE tbAgent.name END as agent  ,  
 tbAgent.mobile , tbAgent.vatid 
from tbBillBody
JOIN tbBillheader ON tbBillBody.parentguid = tbBillheader.guid
JOIN tbLand ON tbBillBody.landguid = tbLand.guid 
JOIN tbAgent ON tbAgent.guid = tbBillheader.buyerguid 
WHERE tbBillBody.status = 'مباع' AND tbBillheader.billtype = 0 
GO
/****** Object:  View [dbo].[vwWorkFeeVat]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vwWorkFeeVat] AS 
select tbBillheader.guid , tbBillheader.buyerguid ,tbBillheader.regdate , tbLand.code , tbLand.blocknumber , tbLand.number , 'رقم الأرض ' +  cast(tbLand.number as varchar(255)) as land ,  tbBillBody.price - tbBillBody.discounttotalvalue as price , tbBillBody.workfeevalue - tbBillBody.discountfeevalue as workfeevalue ,tbBillBody.vatvalue , dbo.GetContractPayments(tbBillBody.contractno, NULL) as Payments ,  dbo.[GetContractRemain](tbBillBody.contractno , NULL ) as remain ,  CASE ISNULL(tbBillheader.buyerdata , '') WHEN 'الوكيل' THEN tbAgent.agentname  ELSE tbAgent.name END as agent  ,  
 tbAgent.mobile , tbAgent.vatid 
from tbBillBody
JOIN tbBillheader ON tbBillBody.parentguid = tbBillheader.guid
JOIN tbLand ON tbBillBody.landguid = tbLand.guid 
JOIN tbAgent ON tbAgent.guid = tbBillheader.buyerguid 
WHERE tbBillheader.billtype = 0 
GO
/****** Object:  StoredProcedure [dbo].[prcupdatelandstatus]    Script Date: 24/10/2023 11:13:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[prcupdatelandstatus] (@landguid as uniqueidentifier)
AS
DECLARE @state as varchar(255)
DECLARE @res as varchar(255)

SET @res = ISNULL((SELECT SUM(csales - cresales) as count FROM (
select tbland.guid ,  ISNULL(count(*) , 0) as csales , 0 as cresales FROM tbBillBody JOIN tbLand ON tbLand.guid = tbBillBody.landguid 
WHERE tbBillBody.status = 'مباع' AND tbLand.guid = @landguid
GROUP BY tbland.guid
UNION ALL
select tbland.guid , 0 as csales , ISNULL(count(*) , 0) as cresales FROM tbBillBody JOIN tbLand ON tbLand.guid = tbBillBody.landguid 
WHERE tbBillBody.status = 'متاح' AND tbLand.guid = @landguid
GROUP BY tbland.guid
) as fixland) , 0)

UPDATE tbLand SET status  = CASE WHEN @res > 0 THEN 'مباع' ELSE 'متاح' END 
WHERE tbLand.guid = @landguid  
GO


sp_addextendedproperty 
@name='LandManageDataBaseName',
@value='%DatabaseDescription%'
GO


GO
DECLARE @retVal int
SELECT @retVal = count(*) FROM [tbEditPassword]
IF (@retVal <= 0)
BEGIN
INSERT [dbo].[tbEditPassword] ([guid], [password]) VALUES (N'9642bbe0-474d-40de-af8b-5d0d2d4e2059', N'')
END
GO

GO
DECLARE @retVal int
SELECT @retVal = count(*) FROM [tbAgent]
IF (@retVal <= 0)
BEGIN
INSERT [dbo].[tbAgent] ([guid], [number], [name], [civilid], [mobile], [email], [vatid], [publicnumber], [agentname], [agentcivilid], [agentmobile], [agentemail], [agentvatid], [agencynumber], [agentpublicnumber], [officename], [officecr], [officephone], [officeemail], [officevatid], [officepublicnumber], [note], [agenttype]) VALUES (N'98368c13-a085-445c-b718-71e43d79bfd6', 1, N'المالك الإفتراضي', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', 0)
END
GO

GO
DECLARE @retVal int
SELECT @retVal = count(*) FROM [tbPlanInfo]
IF (@retVal <= 0)
BEGIN
INSERT [dbo].[tbPlanInfo] ([guid], [name], [ownerguid], [city], [location], [number]) VALUES (N'df04d7e4-e70b-4e90-91cd-8074e412142b', N'مخطط رقم 1', N'd954e911-fd94-4e97-9911-395882f27e94', N'', N'', N'')
END
GO


GO
DECLARE @retVal int
SELECT @retVal = count(*) FROM [tbVatSettings]
IF (@retVal <= 0)
BEGIN
INSERT [dbo].[tbVatSettings] ([guid], [companyname], [vatid]) VALUES (N'6bb31e13-25ac-42ca-be1c-53d87c991a01', N'اسم الشركة', N'1234567890023')
END
GO
	

GO
DECLARE @retVal int
SELECT @retVal = count(*) FROM [tbTaxDiscount]
IF (@retVal <= 0)
BEGIN
INSERT [dbo].[tbTaxDiscount] ([guid], [isworkfee], [workfee], [issalefee], [salesfee], [isbuildingfee], [buildingfee], [isvat], [vat], [isdiscounttotal], [discounttotal], [discounttotalvalue], [isdiscountfee], [discountfee], [discountfeevalue]) VALUES (N'cd92e9d6-296e-4734-ba9b-23f4aa024e60', 1, CAST(2.5000 AS Decimal(18, 4)), 0, CAST(0.0000 AS Decimal(18, 4)), 1, CAST(5.0000 AS Decimal(18, 4)), 1, CAST(15.0000 AS Decimal(18, 4)), 0, CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), 0, CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
END
GO

GO
DECLARE @retVal int
SELECT @retVal = count(*) FROM [tbPaymentsNotes]
IF (@retVal <= 0)
BEGIN
INSERT [dbo].[tbPaymentsNotes] ([guid], [landpricenotes], [workfeenotes], [vatnotes], [discountnotes]) VALUES (N'cfb9b061-5e20-4fce-898a-1a31fcb1117d', N'دفعة من قيمة العقد رقم %العقد% لـ %الأرض% ، المشتري %العميل%', N'دفعة من قيمة العقد رقم %العقد% لـ %الأرض% ، المشتري %العميل%', N'دفعة من قيمة العقد رقم %العقد% لـ %الأرض% ، المشتري %العميل%', N'دفعة من قيمة العقد رقم %العقد% لـ %الأرض% ، المشتري %العميل%')
END
GO

GO
DECLARE @retVal int
SELECT @retVal = count(*) FROM tbUsers
IF (@retVal <= 0)
BEGIN
INSERT [dbo].[tbUsers] ([guid], [name], [password],  [IsAdmin], [CanAdd], [CanEdit], [CanDelete], [CanPrint]) VALUES (N'ad4b6245-5154-4651-9d40-fd9aa3432217', N'مدير', N'',   1, 1, 1, 1, 1)
INSERT [dbo].[tbUsers] ([guid], [name], [password],  [IsAdmin], [CanAdd], [CanEdit], [CanDelete], [CanPrint]) VALUES (N'00eb000c-f5a7-40b3-b519-5ef3f81f0f19', N'مستخدم', N'',   0, 1, 1, 1, 1)
END
GO

GO
DECLARE @retVal int
SELECT @retVal = count(*) FROM tbSMSsettings
IF (@retVal <= 0)
BEGIN
INSERT INTO tbSMSsettings values (NEWID() ,  '' , '' , '' , '' , '') 
END
GO

GO
DECLARE @retVal int
SELECT @retVal = count(*) FROM [tbEmailSettings]
IF (@retVal <= 0)
BEGIN
INSERT [dbo].[tbEmailSettings] ([Guid], [SenderName], [SenderEmail], [Password], [Subject], [MessageBody], [SMTPServer], [Port], [UseSSL], [CCEmail]) VALUES (N'b8ce8eca-0d3f-44fb-92f8-9d521c445599', N'', N'', N'', N'', N'', N'', 587, 1, N'')
END
GO

GO
DECLARE @retVal int
SELECT @retVal = count(*) FROM tbAppInfo
IF (@retVal <= 0)
BEGIN
INSERT [dbo].[tbAppInfo] ([Guid], [AppTitle], [ColumnCount], [BackupOnExit], [BackupPath], [background], [KMStyle], [QtyFormat], [CurrecnyFormat], [Logo], [ShowConfirmMSG]) VALUES (N'379f2aba-3fcb-4013-a90a-de69da029251', N'إدارة الأراضي', 30, 0, N'\', 0xFFD8FFE000104A46494600010201012B012B0000FFEE000E41646F626500640000000001FFE107024578696600004D4D002A000000080007011200030000000100010000011A00050000000100000062011B0005000000010000006A012800030000000100020000013100020000001C0000007201320002000000140000008E8769000400000001000000A2000000C2012BFFD900010000012BFFD90001000041646F62652050686F746F73686F70204353332057696E646F777300323031323A31323A31352030393A33313A3139000002A00200040000000100000501A003000400000001000003200000000000000006010300030000000100060000011A00050000000100000110011B000500000001000001180128000300000001000200000201000400000001000001200202000400000001000005D90000000000000048000000010000004800000001FFD8FFE000104A46494600010200004800480000FFED000C41646F62655F434D0001FFEE000E41646F626500648000000001FFDB0084000C08080809080C09090C110B0A0B11150F0C0C0F1518131315131318110C0C0C0C0C0C110C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C010D0B0B0D0E0D100E0E10140E0E0E14140E0E0E0E14110C0C0C0C0C11110C0C0C0C0C0C110C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0CFFC0001108006400A003012200021101031101FFDD0004000AFFC4013F0000010501010101010100000000000000030001020405060708090A0B0100010501010101010100000000000000010002030405060708090A0B1000010401030204020507060805030C33010002110304211231054151611322718132061491A1B14223241552C16233347282D14307259253F0E1F163733516A2B283264493546445C2A3743617D255E265F2B384C3D375E3F3462794A485B495C4D4E4F4A5B5C5D5E5F55666768696A6B6C6D6E6F637475767778797A7B7C7D7E7F711000202010204040304050607070605350100021103213112044151617122130532819114A1B14223C152D1F0332462E1728292435315637334F1250616A2B283072635C2D2449354A317644555367465E2F2B384C3D375E3F34694A485B495C4D4E4F4A5B5C5D5E5F55666768696A6B6C6D6E6F62737475767778797A7B7C7FFDA000C03010002110311003F00B15056EB0AB55D95BAD5F2E694CC6A335A86C466A085C3522D446848841281CD4273558704270450D67B501ED56DE105CD452D47B105CC56DED417351486AB9AA1B15873526D72504B0AAA929CD7AB47949F9AB7555ED27C940B25C7CF41F009051421A1AD2F77035551C49249E4EA559CB789F45BC37E9FF5BC3FB2AB1454183909C88E427140AE0FFFD0B35156EB2A8D6E56AB72BEE696E30A334AAAC72335C821B01C9C9420E4FB924AE4A1B948B9409454C1C109C115C86E45485C105C1587050D9281296BEC944AEA928ADAA55BA31C92001A94D325D18DA12CF4E99EE7854722E1437DBFCE387B0780FF00487FEF8AD753CCAE977A6D87B9A21ADEDFD67FF256339EE7B8BDE7739DA925188EAA3A95945C539286E29CA62E284F2A6E284E2815C1FFD17ADCAC56F5418F561962BEE790E831E8CD7AA0CB119B624B69B81EA5BD56162907A484FB9317216F4B7A49664A89D53807BE9F1D149A19DDC3F2FE44894B00C254DB4CA20358F13F01FDE88D7F76B401FBCE32A324AE881D56AF1FB9D00E4A0753EA0312BF4318FEB0F1EE7FEE30FEECFF00857FE67EE27CDCE18ECE7759F9A3C0FF0057F35605B639EE73DC65CE324A318DEA5719740C1E753DC9D492A04A673944B948B697250DCE48B90DCE4095CA73909C53B9C86E29A4A43FFFD2A4C7A3B1EA8B5E8CD7ABED121BCDB115B62A2DB111B624B486F36D44162A22D536D89229BA2C1DB5F8A90B0F8FDDA2A62D1F1446D8E3C7E08AA9B63C4E9F1D111AE678CFC152F5AB6F264F80D7F148E5BA3DB0C1E3C94D214E8FA8D60DC61A3C5DFC02AD93D4433DAC92FF0013C8FF00D27FF9F3FA8B3ECCA7132D267BBCFD2FFCC556758908AED525B6B9EE2E71925577BD45F62197229019172897281728172569A64E72839CA25CA05C9A4A405CB94094C4A89282E7FFD3C56BD11AF554394C3D5C05A94DB6D8A62C5503D483D3AD14DC16A98B55216290B11B453785A079948DC4F274F0ECA9FA897A895A29BBEAC283AE2792AA9B531B12B55361D6A1BAC40362897CA169A485F2A25CA1B944B90B4D332E512E502E4C4A169A5CB94494C4A8929269725449489519412FFFD4E683948390D3CAB56D64A1CA41E83B93EE46D14983D4B7A06E4B7256AA4FBD3FA881B92DC8DAA93FA89B7A0CA794AD5493725B90E5294954CF7262546534A4AA644A8929A534A0A5C94D29A52492A94C924829FFD5E6125C8A4AD359EB93AE412494F5E96AB9049253D86A97B971E924A7B1F7A5EFF25C7248A9EC7DE97BFC971C924A7B1F7F926F7792E3D2414F61EE4B55C7A4929EBF54B55C824929EBD32E452494FF00FFD900FFE20C584943435F50524F46494C4500010100000C484C696E6F021000006D6E74725247422058595A2007CE00020009000600310000616373704D5346540000000049454320735247420000000000000000000000010000F6D6000100000000D32D4850202000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001163707274000001500000003364657363000001840000006C77747074000001F000000014626B707400000204000000147258595A00000218000000146758595A0000022C000000146258595A0000024000000014646D6E640000025400000070646D6464000002C400000088767565640000034C0000008676696577000003D4000000246C756D69000003F8000000146D6561730000040C0000002474656368000004300000000C725452430000043C0000080C675452430000043C0000080C625452430000043C0000080C7465787400000000436F70797269676874202863292031393938204865776C6574742D5061636B61726420436F6D70616E790000646573630000000000000012735247422049454336313936362D322E31000000000000000000000012735247422049454336313936362D322E31000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000058595A20000000000000F35100010000000116CC58595A200000000000000000000000000000000058595A200000000000006FA2000038F50000039058595A2000000000000062990000B785000018DA58595A2000000000000024A000000F840000B6CF64657363000000000000001649454320687474703A2F2F7777772E6965632E636800000000000000000000001649454320687474703A2F2F7777772E6965632E63680000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000064657363000000000000002E4945432036313936362D322E312044656661756C742052474220636F6C6F7572207370616365202D207352474200000000000000000000002E4945432036313936362D322E312044656661756C742052474220636F6C6F7572207370616365202D20735247420000000000000000000000000000000000000000000064657363000000000000002C5265666572656E63652056696577696E6720436F6E646974696F6E20696E2049454336313936362D322E3100000000000000000000002C5265666572656E63652056696577696E6720436F6E646974696F6E20696E2049454336313936362D322E31000000000000000000000000000000000000000000000000000076696577000000000013A4FE00145F2E0010CF140003EDCC0004130B00035C9E0000000158595A2000000000004C09560050000000571FE76D6561730000000000000001000000000000000000000000000000000000028F0000000273696720000000004352542063757276000000000000040000000005000A000F00140019001E00230028002D00320037003B00400045004A004F00540059005E00630068006D00720077007C00810086008B00900095009A009F00A400A900AE00B200B700BC00C100C600CB00D000D500DB00E000E500EB00F000F600FB01010107010D01130119011F0125012B01320138013E0145014C0152015901600167016E0175017C0183018B0192019A01A101A901B101B901C101C901D101D901E101E901F201FA0203020C0214021D0226022F02380241024B0254025D02670271027A0284028E029802A202AC02B602C102CB02D502E002EB02F50300030B03160321032D03380343034F035A03660372037E038A039603A203AE03BA03C703D303E003EC03F9040604130420042D043B0448045504630471047E048C049A04A804B604C404D304E104F004FE050D051C052B053A05490558056705770586059605A605B505C505D505E505F6060606160627063706480659066A067B068C069D06AF06C006D106E306F507070719072B073D074F076107740786079907AC07BF07D207E507F8080B081F08320846085A086E0882089608AA08BE08D208E708FB09100925093A094F09640979098F09A409BA09CF09E509FB0A110A270A3D0A540A6A0A810A980AAE0AC50ADC0AF30B0B0B220B390B510B690B800B980BB00BC80BE10BF90C120C2A0C430C5C0C750C8E0CA70CC00CD90CF30D0D0D260D400D5A0D740D8E0DA90DC30DDE0DF80E130E2E0E490E640E7F0E9B0EB60ED20EEE0F090F250F410F5E0F7A0F960FB30FCF0FEC1009102610431061107E109B10B910D710F511131131114F116D118C11AA11C911E81207122612451264128412A312C312E31303132313431363138313A413C513E5140614271449146A148B14AD14CE14F01512153415561578159B15BD15E0160316261649166C168F16B216D616FA171D17411765178917AE17D217F7181B18401865188A18AF18D518FA19201945196B199119B719DD1A041A2A1A511A771A9E1AC51AEC1B141B3B1B631B8A1BB21BDA1C021C2A1C521C7B1CA31CCC1CF51D1E1D471D701D991DC31DEC1E161E401E6A1E941EBE1EE91F131F3E1F691F941FBF1FEA20152041206C209820C420F0211C2148217521A121CE21FB22272255228222AF22DD230A23382366239423C223F0241F244D247C24AB24DA250925382568259725C725F726272657268726B726E827182749277A27AB27DC280D283F287128A228D429062938296B299D29D02A022A352A682A9B2ACF2B022B362B692B9D2BD12C052C392C6E2CA22CD72D0C2D412D762DAB2DE12E162E4C2E822EB72EEE2F242F5A2F912FC72FFE3035306C30A430DB3112314A318231BA31F2322A3263329B32D4330D3346337F33B833F1342B3465349E34D83513354D358735C235FD3637367236AE36E937243760379C37D738143850388C38C839053942397F39BC39F93A363A743AB23AEF3B2D3B6B3BAA3BE83C273C653CA43CE33D223D613DA13DE03E203E603EA03EE03F213F613FA23FE24023406440A640E74129416A41AC41EE4230427242B542F7433A437D43C044034447448A44CE45124555459A45DE4622466746AB46F04735477B47C04805484B489148D7491D496349A949F04A374A7D4AC44B0C4B534B9A4BE24C2A4C724CBA4D024D4A4D934DDC4E254E6E4EB74F004F494F934FDD5027507150BB51065150519B51E65231527C52C75313535F53AA53F65442548F54DB5528557555C2560F565C56A956F75744579257E0582F587D58CB591A596959B85A075A565AA65AF55B455B955BE55C355C865CD65D275D785DC95E1A5E6C5EBD5F0F5F615FB36005605760AA60FC614F61A261F56249629C62F06343639763EB6440649464E9653D659265E7663D669266E8673D679367E9683F689668EC6943699A69F16A486A9F6AF76B4F6BA76BFF6C576CAF6D086D606DB96E126E6B6EC46F1E6F786FD1702B708670E0713A719571F0724B72A67301735D73B87414747074CC7528758575E1763E769B76F8775677B37811786E78CC792A798979E77A467AA57B047B637BC27C217C817CE17D417DA17E017E627EC27F237F847FE5804780A8810A816B81CD8230829282F4835783BA841D848084E3854785AB860E867286D7873B879F8804886988CE8933899989FE8A648ACA8B308B968BFC8C638CCA8D318D988DFF8E668ECE8F368F9E9006906E90D6913F91A89211927A92E3934D93B69420948A94F4955F95C99634969F970A977597E0984C98B89924999099FC9A689AD59B429BAF9C1C9C899CF79D649DD29E409EAE9F1D9F8B9FFAA069A0D8A147A1B6A226A296A306A376A3E6A456A4C7A538A5A9A61AA68BA6FDA76EA7E0A852A8C4A937A9A9AA1CAA8FAB02AB75ABE9AC5CACD0AD44ADB8AE2DAEA1AF16AF8BB000B075B0EAB160B1D6B24BB2C2B338B3AEB425B49CB513B58AB601B679B6F0B768B7E0B859B8D1B94AB9C2BA3BBAB5BB2EBBA7BC21BC9BBD15BD8FBE0ABE84BEFFBF7ABFF5C070C0ECC167C1E3C25FC2DBC358C3D4C451C4CEC54BC5C8C646C6C3C741C7BFC83DC8BCC93AC9B9CA38CAB7CB36CBB6CC35CCB5CD35CDB5CE36CEB6CF37CFB8D039D0BAD13CD1BED23FD2C1D344D3C6D449D4CBD54ED5D1D655D6D8D75CD7E0D864D8E8D96CD9F1DA76DAFBDB80DC05DC8ADD10DD96DE1CDEA2DF29DFAFE036E0BDE144E1CCE253E2DBE363E3EBE473E4FCE584E60DE696E71FE7A9E832E8BCE946E9D0EA5BEAE5EB70EBFBEC86ED11ED9CEE28EEB4EF40EFCCF058F0E5F172F1FFF28CF319F3A7F434F4C2F550F5DEF66DF6FBF78AF819F8A8F938F9C7FA57FAE7FB77FC07FC98FD29FDBAFE4BFEDCFF6DFFFFFFDB0043000202020202020202020203020202030403020203040504040404040506050505050505060607070807070609090A0A09090C0C0C0C0C0C0C0C0C0C0C0C0C0C0CFFDB004301030303050405090606090D0A090A0D0F0E0E0E0E0F0F0C0C0C0C0C0F0F0C0C0C0C0C0C0F0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0CFFC00011080320050103011100021101031101FFC4001F0000000701010101010000000000000000040503020601000708090A0BFFC400B51000020103030204020607030402060273010203110400052112314151061361227181143291A10715B14223C152D1E1331662F0247282F12543345392A2B26373C235442793A3B33617546474C3D2E2082683090A181984944546A4B456D355281AF2E3F3C4D4E4F465758595A5B5C5D5E5F566768696A6B6C6D6E6F637475767778797A7B7C7D7E7F738485868788898A8B8C8D8E8F82939495969798999A9B9C9D9E9F92A3A4A5A6A7A8A9AAABACADAEAFAFFC4001F0100020203010101010100000000000000010002030405060708090A0BFFC400B5110002020102030505040506040803036D0100021103042112314105511361220671819132A1B1F014C1D1E1234215526272F1332434438216925325A263B2C20773D235E2448317549308090A18192636451A2764745537F2A3B3C32829D3E3F38494A4B4C4D4E4F465758595A5B5C5D5E5F5465666768696A6B6C6D6E6F6475767778797A7B7C7D7E7F738485868788898A8B8C8D8E8F839495969798999A9B9C9D9E9F92A3A4A5A6A7A8A9AAABACADAEAFAFFDD000400A1FFDA000C03010002110311003F001CA3BE7A13E6EAEA3F0C555D462C510A3F0C5510A3C70A95751D3162510A3FAE06288518AABA8C08288518A110A3154428EF818ABA8C05510A3F0C0AACA3F1C5055947E18AAAA8EF8AF9AB28ED8AAA81F862AAAA3BE2BE6AABFAF155503B6295502B8AAA818AAA01DFC7155E077C0AAA060E6AA80773D3155E063C957D298AAA05A75C1CD57004E2ABC0F018A69705C6D6D753C314B7C4E286F88C0ADD07862ADEFE18AB7BF862B4D6FE18AD3B7F0C569AA0F0C52EA0C285A41F9E2AD52BD7155BC7C31B55A462AB0AF861B559407D8E3C90B48F1C36953229F2C285847860558462AA647DF8556118AA991DBBE2AA6462AA6476C554C8C554C8ED8AA9115C554C8C55488EF8AA991855488C554C8ED855488FBF155223BE2AA447DD8AA9118A5488C55498530A54597155261FD98AA8B0FED18AA8B2FF00B78A42911852A2C3155265C52A4CB8595A911F876C0952237C2AA44605532BF76295223155323155265C554CAE29532315532298A415323164A657FDAC554CAE2AA647862AB0AE29584615B532B8A56118A54C8C556115C0AB08FF006F155A478E2AB08F0C09B58462AB08F1C556918AAC23E838AADA1C55AE3ED8AAE0B8AB617E9C55785C55785C555557142B2AE042AAAFFB78AAB2AF4C555D570A1128B8AB576B4B693DE83F1C4284A517A61648B8D3E8C504A3234C58A1755DA0853BB39623D947F6E1098F349D57A6499221571422117A62A8854F6C516AD28E3022F795EBF42FF69C5014157FDBC536AC17142BAAE2AAA171415455FBBB628560B8A1542FD1ED8DAA5DACCFF57B21083492F1B8FCA35DDBEF341846E59406EC3F24DAEC4AB47A62AB315762AA67AE029763D10A580A56B76C5569C5214F14C961DF0325ADD707442C3D322AB0F518A42C63F86292A58AA99C554DBA9C09587BE29E8A780A4299C0554F1A57FFD03051D33D09F374428C50AEA314221462AAEBFAF0A110A3020A21462C55D462A8851818A25462A88518AA2146062AEA3FAE02AAEA3A7DE702AB2F8E285603B62AAAA2B8AAB28C55540ED8AF92A818AAAA8EF8AAAA8FBF155503B786295503F0C0AAAA3BFDD8AAA01DF0155E07DD8955402B8F255403C31E4ABC0A7CF02AF0B4F7C557818A697018A5785F1C50BBE58AB7C4F8E055D418AB7F218ABA8714B7438AB543855D43E18ABB022969030A5AA1C55AC556F1F0C50B48AF518AAC2298AAD23EFC55611E38556114C2AA6477FC30A1611E18154C8AE2AA6457E78AA9915C2AB08C55488AE2AA6457E78AA9915C554C8C55488EF8AA991855488C554C8C2AA447DF8AA9118AA991DF1552231552230A5488C554987D2314A911D477C55488C554597EFC554597EEEE314A8B0C29E6A657155123155265C52A4462C86EA446155261F8F7C0953618AA915FBB1552618A54CAE2AA647862AA6462953230AA9918136A646164A64605532BF7E2AA657EFC2AB48F1C52A647DD8542C2B812A447DF812B08C556D3C7EFC50B08A7B8C0AB287B6295857E838AF25847638A5691E18AAC22B8AB5C715771C55B0BED5C557F1C55705F0FBF15B540BEDF4E042A85DB142AAAE2AACAB8AABAAE285745C2A8945F6C554EF9696F4FE675FC2B885096A27B614A3234F6C58A3234C56D2AD5B79E18FF00923A9FF64724131402AE14A21171528955C58A215302B7723F7A23ED0A84FA7AB7E2710A14D570AAB85C555557A628B5555AE285655C50ACABD3F5E2AAE9196214753B6450C1F57BA1757D2321AC30FEEA0F755EFF0049A9CB006E88A0966164EC55D8AA9E2AEC554CF5C052EC7A214B014AD6ED8A7A2C3D0E2A16604959E1894AC3F6B07455A7BE048584E05523BE295876C554CF5C554DBA9C0958714F453C052A47A1C05566157FFFD1325CF427CD910A3155751FD30A15D474AE054428C2C7BD128302110A3142BA0C50512A30215D474C5512A3142BA8FC7014221464555D46DF3C50ACA3FB715560315B5551D3155651F862AAAA31555518AD2AA8AEF8A5540FBCF4C55540E83F1C55540FC302AA815FA3072554031E4AA8063C95500ED8392AF031554029F3C79AAF0B8B25E057142E1B74C095FC7C714363C062ABB8E14AE000C55D8AB743E18ABA8715750E2AEA1F0C55AA7B62AD5062AD71F0C55A3EF8AAC2BE18AB4478E2858411EF8AAC231E4AB48EC70AAC2310AA646155847DF8AA9918A14C8AFCF155323EFC2AA647DF8AA991F7E2AA647DF8AA991DF155223BE2AA6462AA646155223B63CD5488FBF155323EFC2AA4C3BE2AA6462AA4462AA446295223FB70AA911D7C3B6295223B7718AA911DF155223155261F7E2AA2CBEDB629512B8A79A915C2AA6CB8AA891852A4CB8195DA911FED6155320604A915EB8AA995C2AA4462953618154C8FBF155223DBE8C2958571553207F6E2953231A482B0AF8E295323095532B8154C8C55611812A4475F0ED8A6D611F4FBE295845715594C55AA78602858476EFE3816D611E38A5611852B69ED8ABB8FB62AEE3ED8ADAE0B8AAE0BE03021785FA715B550B4C50A8062AA8AB8AABAAE285755C2A8944E98AA2913A62850D4169144BE2E6BF40FEDC21420113149464698A11B1A628258F5F9E77B391B856E03FD880324190E4A28B8528954C58A2553154640839866FB3182EFF2515FE180941416EEC5986EC791FA70A55557DBE8C5555571636ACA98A15957155709F4646D0AAA98A10DA95C7D4AC26941A4B37EE6DFFD661B9FA0570816CA22CBCF88A0CB1B815B8A5D8ABB1553C55D8AA9B75C052D1E989E4AA78156B62AB7A6290A7812566252B0F5380F255A7BE0559812A67A9C52A6DD4E2AA6712AA4702561E98A7A299E980A54CF4380AA9E04BFFFD23451D3DF3D09F364428C5510A30A15D7C302ABA0C2C4A25060422146285741F8E04144A8C5088518AA21474C0C510A30154428FC702AB28FC3142B28C55580FC315565F1C55540E8315565EB8AAA81F8E2A1540EDE18A5540FECC55540EE702AA814A789C55500A63E6AAA074C1E6AA805300554514C55500A63CD57814F9E295E16BBE05B5E057A62ABC003155C0570A5774C557018AB7C462ADD3C062ADD0F862ADD0E2AEE27156A87C3156B1568818AB5C7155A4788C55695F0C556918AAC2298AAD23BE2AB08AE28584611BAAC230AAC23155323155323C31429915C554C8AFCF1553230AA911DF155323BE2AA6462AA4462AA6461553231552230AA9918AA911DF155223EEC55488C52A6C30AA8B0FC714A991F8F4C55488DBDC62AA4462AA4476C55488FBF155261FDB8AA932FDD8A5448C290A6CB8AA9118AA8B2E29522B8590DD48AE2AA647FB78A54D860552230AA9B0C095223FDBC554C8ED8AA991852A6569D3BF6C554C8C904AC23B1C52B0AE452A6462AA647DD8154C8A62AA646295847874F0C536A657C314AC201C50B48A6055A462AB08FA462AB38F86297713ED810EE2714B7C478E2B6B82FB628B5C062ABC0C55785FF006F155555C555957142B2AE154422F4C5514898AA2D1314213515DE05F663F88C428422261546C698A946C6A14F33F654163F21BE2C187EEECCE772E4B1FA4D726DA8854E98B1442262A8944C555D87A76D21EF3308D7E43E26FE183AB1EA8455FBF0A55557A628B5755C50ACAB8AAB2AE0285754C08565424D00AD7A0C0AC33CC37627BE16E8C0C3640C629DE43F6CFDFB7D196446CDB1141213D0E49929E2C9D8ABB15587A9C55AC554DBAE02968F4C4AA9E055ADD715587A6298ACC597559DF225561EA7015587129587C702D29E295363D71553C554F0254DBA6292B0F4C09533D0E44A54F157FFD336519E84F9B22146284428E98AABA8FBCE282885185894420C084428C55108303144A8C551083154428C0C55D074C8AA21462AACBFAF142B28FC3155651F8E2AACA315555F1C55580E98AAAAE2AACB8A5500DC0F0C55597F574C055517C7BE02AAAA315555C4EE5551477C0AA8077C4AAA01DCFD18A697815C0AA8057155F4EC3155C0614AE02B8AAF000C557018AB741815BC2ADD0F862AEA1F0C55D438ABBA62AD62B4D52BED8AB5438AADC55691E18AAD2315584118AAC23155A462AA64570F355846142C2298AAC23BE2AA6462AA6477C554D877C55488EF8AA991DF0A14C8C55488A62AA6462AA6476C2AA4462AA4477FBF0AA991DC62AA4462AA645314A9118AA91F975EB855488EDE3D314A911DBEEC5549862AA647DD8AA911DB155223EFC5549877C554C8EE315512BD714A911BFEBC29522315532315512BF7614A915FF6F02A911FED61640A991DB14A9B0C0AA64771855488C52A6462AA647E3DF1553230A54CAFD38AA991852B08A6252B08EE3EEC8A5488F0C0AA647862AB08F0C554C8FBF14A9915F9E2AB08AEDDF0256914C556118AADC0AD5062AB78E15771C0AB80C557007155C062ABC2E2AA817155555C285655C5510898AA2913DB15452262A8C8D3A6025097EA0B5B845FE58C7E249C21428A274C2A8C8D3A62C495F767D2B1B97E84A705F9B9A7EAAE3D5431744C9B2442A74C16A8955C04AA2157030B5D7838BC50F6857E3FF59FE23F76C3241210EAB8555D57142B2AE2A8855C8945AB2AE042B2A74C554AFAE869F653DDD7F78A385B0F195B65FBB73840BD931165E65527726A4F53E396B73B15536EA71641AC55D8AAC3D4E2AD62AA67A9C052B5BA625566055ADD7152B0F4C59454CF4C0BD568C052A63192B4722A54CF4C59291DE98AAC38AA91E8702A99E98A56378629536C8F54A9B76C09598ABFFFD4385FE39E84F9B2BA8E9850884C0AAEBDB1544A62C110A3142213AE28442F6C0844A0C5510BDBDF1422147E380A1108322AAEA3F1C50ACBDBDB155651F8E2AACA3F0C55557C7155651D315565C55540FEDC52ACA37F962AACA36F9E05540BD07DF83CD554780C792AA018F25550302AA815FA3155402B8A42F02A702DAA7B0C557F4DB155E053E78AAF02BF2C295E076C557014C0AB80AE156F8F8E04AEC55C3AE15A5C715A6862B4E3B62B4B7AEC714B5C7C31435438AB4478E285841C556915C5561C556914C554C8EF8AAC231B55A4572485323B62AA6476C1C954C8C2AB08C55488C55611F8E2AA4462AA647E38AA9918554C8EC71552231E485223BE1095361DF0AA9118AA9918AA9118AA930C5549853E5DF0AA911DBEEC52A647DF8AA930FC7155261FD98AA911F78C554C8FBF15522315522298AA9118554987DFDC62C94C8C55488AE2AA647DFDF0AA8B2E29522302DA991BE2C948AE29532B8554C8C554C8C0AA6C3EEF0C212A4453E5852A6453155845724AA6476EF80AA9919129E4B08EF81929915F9E2AA6462AA647D18AAC230256107FB7155323C714AC229FD7156B02B5418ABA831575062ADD3DB155C17155E17155E17142A85C2AAAAB8AABAA62A8944C55148BD36C55191A602846C71E449B54A6F456EE6FF2485FB801931C956A274C28251B1A74DB031426B078DB5BC5FEFE90B11EC8283F16C21212244C91648955C8A9348844C58928EB78D4C8A5FEC202F27FAABB9C58A5EC5A591E46FB523163F326B930192A2AFF00B78A15D53022D5D5712AAEAB810ACABFEDE055754AEC07D031412C27CCF7A26BC5B189AB0D8555E9D0CCDF6CFD1F672C80EADD8E3B5B1AC93321D8A16375C5216E29762AB0F538AB58AA99EA71569BA602953C0AB0E2A5631ED8B28AC3D3151CD6763913CD2B3015587B6049587A629533FAB15536E98AA99E98154CFF001C4A4299EB8A7AAC6ED914A93604ADC55FFFD5395EDF2CF427CD910B850885C555D7C7142213F86062895C50AEBDB02112A314225462A884F1C50510B9128442E055751D3142AA8EF8AABA8FC315555F1C555969B62AACA3155651D314AB2E2AAA3A0F7C55540E9E180AAA800D4E2AAAA30735555189E6AAA05060E6AA80507CF1E690A805314AA0141810A8053E9C55701DF14AA015C2ABC0AE055E053155C078E2ABB14B74C6D2DD3155D438D2DBB89C695D43E18D2BA98D2B4462AB4838AB5858D358B20B3AE2821A23142CEB8AAC231558462AA6477C55611DF10AB08AE155322B8AA991850B08C55488C554CE2AA6462AA6462AA6461552231553618AA9119255323EEC55488ED8AA991DB15522298534A446FF003EF8AA991DBBF638AA8B014F962AA6478FD38AA930ED8AA991DB155223EFC55488EF8AA991DF155223F1C2AA4C3B7862951618A54C8C554C8FBF155323EFC55488C2AA4C3EEC52A647DD8A82A45703253230A54D97155322B8AA991F7E482A911FED604A911DC614AC23BE055323FDBC16AB08FA7DF02A991F7E295322B8B258462AA646055323155323C714AC23155B4F0C0AD71F6C52D71F6C55DC7DB15753DB155D4C55705FF6F155E17142A018AAAAAE155655C55128A71544A27B60422D131B546C6990547431D59453A9031563F27EF2699FF9DD8FDE72C0AAC89B8C58928C8D3A62849B593CAF1621D2DE255A7F94DF11FD78625904BD5714A21171608945C551327EEAD1CFEDDCB08D7FD55A337F0188E68402AE4D2AEABD30142BAAE042215302AB2A6155755C5892A37F7834CB19EF76F557F7768A7BCAC0D3FE047C47102CD2611E22F2824925892CC4D598F524F7CB5CA6B152DE2C56376C5216E29762AA78ABB1553C556B644A566291CD4F1415ADE18198536ED850169E991EA95981561EF8125613B629526DF15536C554DB02561ED8A8533D7148536EB914A91EB812B790C55FFFD63A5EC33D09F36442F4C2844276C5510B81892884C58A217154420C082895FE38A1129FDB8AA2146062885C0555D7B6052AEBD7142B28E9EF8AAB2F5C55597B62AACBD7155603A0C5559714AA81D3155651BE05555EBF3C5554540DF10AAA064477AAAF80C472555037C7A2AA0C52A8A3F0C095402BF46285E3738A553AE2ABC0E830AAA014C0ABC0C55B1E18A57D31E690B80C5570C6D5BA1C1455BE38D2B5C71A5750E34AD11DB0DAB54F7DB1B52B08AE1435C714AC23E8C556FB6282B08DF155A7142C2315532315532298855A477ED92A55323BE2AA6477C1CD54C8FEDC2154C8EF8A14D877C554C8C554D862AA446155322871E6AA646FF4610AA446155323155222A315532314D2911BEFDF0AA911D47874C55488EBE23155361FDB8AA930DF155223B62AA6457E8C55488C554C8EF8AA911855488F1C55488FBC62AA641EBE3DB164A44628532314A991855488EFF7E2AA447E38AA9B0C52A4460505611BE2C94C8C29532BFD98554CAFFB7855488C0AA64629B522B82D54C8C0958462AA647DD8AA9918A54C8AE2C82C23B6055323B1C554C8ED8A56118AADA62AEE23156B8E2AEE3815BE3855B03DB155C17C715540BF462AA817E8C50ACAB81510AB8528854AE2A8B8D3DB204A1191A60546C69D31546A0E0AF21E91A337DCA4E2AC69136DF2C6368B44E98A131B78833A29D949F88F801B93F7602AC36790DCDCCF707FDDD2338F913B7E19266B9531624A2913A62844A213400549E8314357FF00DFAC0BBADAA88F6EEDD5CFDE69863C92A0AB922A8855C8B15745C0AAEABBFEAC5055D5316368848CB10AA2AC76503B93D3143CFBCD3A82DDDF0B485F95B69D58C30E8D29FEF1BEF141EC32C88DADC9C71A0C6324D8EC55BC58AD6C5216629762AA78ABB1553C1D52B1BB62AB7B604C54F1415AD8B3536ED8142C634030055B8154CE04AC38A4299C554CF862AA6D812A6714853F1C0A161EB812A27BE02953C55FFFD73B5ED9E86F9B2BAFEBC50894C0A885ED8B12894ED8B15751D31544276C082895ED8A11098AA2506062510B81510BD46042B2F4C555D7AE2AAAA3155751BE2AAAB8AABAF5C5555474C52AC3F562AAAA3155603A0C0AA8BBE0E8AAAB8955651DF0155451B5712AAA3A6252A8361815500A0C557814C295403155451DF155E06055D4C2C82F03B60E6ABC0F1C4AAE031A55DB7863C95BA1C1696F89C6D5BE3EF8DAB54C36AD1F7DB142DE3E18D2AD230AAD231558462AB08FF6F142C2298AADA57E8C50B48FC71553231558478E2AA78AA991D8E4AD54C8C0AB08C2AA67DF1429D3B62AA4476C55611DB15532315512308D954CF4C245AA99FF006F1A4A991855488FB8F4C42A911D47DD8AAC22BB786155122A3DC77C554CF8E2AA447518AA991D462AA447DE3155223BE2AA6C30AA9118AA911DB155222BF3C52A6477EFE18A1488C595A9B0FC3155322BBF7C55488EF8554C8EF815488C2AA6476C55488ED812A6478E14AC230A42C2B8D25488FECC2AA647F662AA4C301553232295323155323EFC52A4462AA6453155847DD812B08C5902A6478E29584531559C7C3156B89C55AA0F0C55AE231577118ABA83C3155C17155E1715540B8155953A63685754C16A8844E9892A8A44E9B64551489D36C55191A74DB1546C69D36C555AE070B2BA6FE65541FEC987F00700E6A792448996B0462262AAB70DF57B0BC9BA308FD38CFF009529E3FAAB91E652187227F664D0514898AA29131547DB011F3B961F0DAA99287BB7441F4B1181094852C496DC93527C49C9F24A21530315754F0C0A8855C516AEA98B15754C5083D62FFF0044E9B2DC21A5D5C560B11DC311F13FFB007EF23081659C2365E4797F3729AC8ABB156F162B1BB7D38A42DC52EC554F1568F438AACC0156375C09587A1C55662AA78B32B0F7C0A16B1DC647A2AD38A54CE050A6DD4E2958C7155238AA99EB812B09D89C53D14FC7014852380AA91E9812B3157FFFD03C5CF427CD95D474C50895EB8AA213B6160884C08442F4C551098108A4C50884E9814A2131625109DB02A217BE042B2F6C5559715565EDEF8AABAE2AAAB8AABAE2AAABDB14AAA8EB8AAB0ED815546C4E0E8AACBFAB13CD5557A63D555474C7AAAA81DB0055403718B25402A69810153AE29541BE15541BE2AA83C3155E36C090B80C4A57D3A1C6D5500E87101576F80955E0531A4B6057156E871B5750E1B56B1D95A22B8D2ADA531055AA0C2858462AB08C556118AD2C23155846D8A39B446DB7D18A14E95EB8AA9918AA9918AAC22BF3C3CD56115DB085523D0E2AA6477C554CF8F86285361B57155361DF0A54CF5AFDF88553237AF8E2AA4C284F86482A911FD98DAA991D477ED8AA9918AA930AEFE1855488E87EFC55611F8E2AA2462AA6462AA6477C55488EF8AA9918AA9118AA911DB0AA9915F9E295222B8AA991F8ED8A1498536EA3B62C94986285323BFDF8A54D87F6E155323155223B62AA4476C52A657142991D8E148EE58476385214C8C52A6476C52A6C3FB3012AA446055323B77C554C8C09532314A9918AA911FED6055323155845714F3594F118B205695C52B0AFB53156B8E2AD50E2AD71F6C55D4F6C55D4C55705F6C0AA812BF2C6D0ACA9FDB80955755C0AAEA98151089D315452262A8B44E98AA3635C16A8C8D3A1C055DA88E3670A779A624FCA35FEAD861CD4F24A5132C608B44E980AA0B5C6E16B676C3ACCED3B8F65F817F1E58C79A52044C9A11289D3142291315445E522B582DC7DBB83EBCBFEA8AAC63F59C039A025EA9925442AFF00B58AABAAF4C58928854C08442A74C50898A22EE105057AB1E807724F801BE0B57967983545D575177849FA95B0F46C878A03BBD3C5CEF96C45072631A090B741930CC2CC4A5D815BC58AC6F0C5216E2968F438AACC556B7EBC556E2AA67A9C8A56B74C57A299E98A42CC525677C052B0F5C0792AD3915598A429B77C5254F155238AA9E04A9B74C52561E980A54CF43915526C52B3157FFFD13C5FE19E86F9B2217B0C0844AE283C95D7F86283CD12A36C58A2171544274FBB02112BD7142217AE2A512A30312885EDF3C8AABAE2AACBDB15575C08565C2AACBD31474561D714F5575E83150AABFC314AB0E98AAB0EB815557615C55557A7CF0755541D3005571D463D155075C52AABD4E05E8A8BDF155EBDF14AA2E15545C5578EA3155418B25C0600AA8057073554F6C24AAF005305257529D77C6D576056E98AB58AB54C2AD529882AD61558450E2156915C28598AAC2298AAC3D7142991DF14ADC5056B6DBF8E2858477F0EB8AA99DFE5885532298AA991BE15584615532315533D71429914C554CF5C5548F876C295223AE3CD54C8A8C900AA67F1C0AA6DBD30AA91F1C2AA6462AA4475C554CE2AA4477C554D877C554D877C55488FC7155323B62AA4476C2AA447DE3155323F0C554D81AE2AA4462905488EC7E8AE2AA6C314A9918554C8FBB155223155223B6295323C70A14C8C554C8FBF1A4F3584614F35323B6290A6476C52A447E182D5488AE055323F0C0AA646295323BE2AA64604A9918AA991DF155323B8C556115C597BD691E38AB58A6DA206295BC4F8E2AEE38AB7C7021B0BBE2AB82FD38AAAAAE02555D5722AACAB8AABAAE2A8944C551689D36C55171A60B546C6BD3032A474698150DAB0A4B6B0FF00BEE00C47BC84B7EAA64E0584D0289D32458A3638CB10A0549D80F9E05637AD4826D52755358ED42DBC67C7D21427E96A9C94392941A2649514898A0A616B6FEB4B1C64F156DE47FE5502AC7E803224A101732FD6AE659E9C55CD235F0451451F400324052B4A985510AB8A0A25530315755C508854C0A521F356A5FA3B4E5B185B8DE6AAA7991D63B6AD09F632114F903E39280B3EE6CC51EAF2E196B7B47A1C2154F12C9D815BC58A9B7538B20D62AB5B155B8AAD6ED8AAC268301553C095ADD71495A4D314C548F438AF55BE272252A7E38955872252569E98A85238A54CE2AA64D06055338A42C6C53D54DB229526C0AA6DD714ADC55FFFD23D5CF427CD9109DB142217C715575FE18B13CD14BFC7162AE3B62A894FE9810895F1C50AE9DBDF152894C058A25722AAE3A6282ACBD462BD55874C517B2BAF5C52ACBDB14772B2E29EF565ED8A8555EE7C314AB81D31555077C0AAA3A1C555476C0155476C1D155875F962AA8B8A42AAF7C0AA8BD0E2AA83A614AA2F4C555074C5551702AE03BE2592A0C4AAA2F4AE21551707355E07E1894AEFE38AAF03C702AEE3EF8A69AA0C514B694C2AB48AE12156D29801568EF84A14F10AB4F8E1558462AB3155845315587638A02C3B8F962A169DC62853A75C4AAC230AA991D8E1553C554CEF8AA9E285338AA9918A54C8FBF14291184254A9F8E48AA9918F355323EFC554C8C2AA4477C554D862AA445298AA991F8E2AA6462AA4476C554C8ED8AA9115C2AA6457155223BE2AA647718A54C8DCE285261FD71480A6457E8C554C8FEDC554987DC70A54C8ED8AA991855488C0AA646155223BE15584629A5322B8A46EA646290A6475F6C0952230155223155323BE05532314AC23C302A9118A56118AA911F762AB08EF8AAC2314F35A4118A56D315B6B8E296B89C55DC4E2ABB8EF81577138052AF55C4AAB2AE4555957155655C55108B8AA29171545A2E0B4808B8D3DB02691D1A74C5298C31166541F6988503DCED902552BD4489B50BB61BA2C9E9A7FAB1FC03F56591D83096E5491324C130848B649AF18545944D36FDD97641F4B11913BEC90C0154B1E4DBB13527C49EB9721148BD31422E34E980A11D2116D6123F496F4FA117888D68643F4ECBF7E0E6552755C9AA21571624A255302110A98A1108981055CB5BDB43717B76C52CECA332DCB0EA40D822FBB1A0180A622CBC5B53D427D56FAE2FEE6824B86A841F65106CA8BECA00032F0285394050A40E14B47A1C42A9E12C9D815BC58A99EA71641AC556B62AB7155877389558DD3014ACC0AB0F8E2A4AC6ED8B30A6D8A85A761F3C8F555980AAC3FAB02561D86295227B62A16376C5549B02AC38A4299DCE290A6DD7229533D702548F5C554F91C534FFFD33D4E99E861F36442751810895C2A55D7A0FA30312895FE38B1575ED8AA25303144AFF1C5512BD17E8C0A885FE38962884EB912AAEBD3141E4AEBD7152AABDB15575EBF762A559702F72B2E15EF565ED8AAB2EC0E29565ED80AAB2FEAC5551715561E180725551D460E8AAABD7E8C7A2AA8EF8A42A2F43815547418AAA0E8314AF1D30AAA8ED8AAF1B0C095E31EA9541B6D89554E8312AA836C012A836A6055C3AE1554EF4F1C0C97F1FBF14ADE9B6285A45714153C95A49D96B602C56E10A56B6142D3BE2AA78AAC3D71558D8AAC3B8C50B7152A5E34EF8A95ADDF0A14C8E98855ADD7155361DF0AA9B62AA6477C50A645314A9918AA9918A149850FB648254C8EE310AA6711B2A911925532298AA9914C55488ED8AA991DB155323B62AA446D8AA9918AA99185549862AA6477C55488FBB155323EEC554C8247BE2AA64577FBF148523B62AA6476F1C295223B62AA6476C3C954C8C55488C2AA6462AA6C3FDAC554C8F0E98AA9918B2A5361894A911DF229533D7E8C55488A60558477C554C8C554C8A604A9918AA9914C52A647DD8AAC23C31558462AB48C0CAD6D062A376B89C534D7138AB7C7155C17DB1B5540BF4E455555702AAAAE2AACABED8AABAAE2A8845E98AA2917DB014808A8D303247469D31547C49D3224AA6F64A2393D761F0DAA3CEDFF003CD4B0FC40C81EE643662D1A93B9FB4773F3CBDA518898094A1F5B736FA5C500DA4D426A91FF001541FD5CFE18C39FB912E4C4D132EB608B8D301628E82079648E28C7292460B18F1276180AA96A32A4B746384F2B7B5510C07C42FDA6FF0064C49C311B250CA9D36C2C4944AA62C510AB8AA2113022D129192400092C6800EA49ED8A181F9DF560D2A6816AE1A1B17E7A948A6A24B9A53857B8881A7FAD5C9E31D5C9C71A0C072C6C762AD374388553C2593B0296CF4C58A962C9D8AAC3D7156B1553C5563644A569E98A429E285AD8B23C949BAE290B5BB6442AD3D322AA6714AC63F862952C554DBAE2AA67AE04AC3DF148533DF0150A64F7C8A5489EA714A99E98A853C55FFFD43D4E99E861F364429C0844AE152AE9DBE8C0C4A257F8E2C55D7B62A894C0844AFF001C50895E8BF4605442FF001C4B144275C895575E98A0F2575EB8A9555ED8AABAF5FBB152ACB817B9597AE1477AB0ED8B20AC3A62AACBDB02AB2F7FA315555C5554601C955475180725551D7E8C3D12AABD3025557A1C08541D062AA83A0C5578E9852ACBDBE58AAF1B8C52B876C1D52557DF12AA989554C1D12A98855CBDF1293CD53DFC30325F5F7C50B09A9AE34AD62A54BB9C90434712A42CC2A4347A628598A1662AB1BB62AB0E2AB3152B0F862AA67C71405A7B1C2142C6C7AA02C3D3095587718AA9F5C50A6715533D0E2AA6457155323638AA9115F98C252A7D4530955222B925533D31553236C554C8DBE58AA930C554D877C554D877C554C8EF8AA9918AA911BD30AA991DB155223B62AB08F0E9E38AA9914F7A75C55488A6FF007E2AA64787438A5488AEDE18554C8DBE5842A9915C295322B8AA911DF155323155323BE2AA4450E2AA64531558462CB92930C09014986D812A6462AA6476C0AA6478F4C55611434EC7155223B60B4A991DB15532314AC2298AAC23C31558457155A47DD8A85BC46066035C715A771C2834B82F80C8A57F1F1FC3020AA05DB02AA2AFB6142B2AE064ACAB8AABAAE2A8A45A53014D22913DBAE04A3234C528E8D3225691F1260648F987A3A55F4BD1A768ED93FD91E6DF826446F2093B458EC6996DB4A3A288B10AA2ACDB28F127A60563DE61944DAA49021E5169EAB6B191D094FEF0FD2E4E598C6DEF632E6952264D8128B44C084CA326D2CEE2F7A4AD5B7B3F1F51C7C6C3FD55FC48C1CCD2424489D0532C624A2913A62C512AB81510898104A2553142175AD54797B4A6BE560351BCE50E8E9DD586D24F4F08C1A0FF002A9E1800E235F36CC71B36F0EDCD492493B963B924F727321C9762AEC55A6E9842429E04B631415ADD3150B314B476C55662AD1E98AACC012A67738156B1ED8A7A2CC54299F1C525675C052B0F5C0792AD3D3229595DFDB14291DEB8B2533DC62AB09A9C554B02561E98A561E980A548F4C055498FE3812A6DD31559815FFFD53D5CF427CD9109DB1422170A0A217F86041E6895FE38B1571DB1544A7F4C0844AF8628574EA2B8A944AE02C5129912AAE3A6282ACB8AF5561D31456CAEBD714AB2F6C51DCACB8A42B2F6C555976AE29565ED8AAAA9A1C0AAAB8AAB0ED802AA8EA30745565EA7E58F45545EF8A42A2F7C0AA83A629541D30AAA0E98AAA838AAA0DC6295CB812AB5AE2555312AA831095C3A645570DB0AAA035C090DE10593A9892AD134C0C4A9E4805029A3B0C4A95985279347A62C56628587AE2AB4F4C5561E98AA9E2AB0F5C542C384202C6E9F2C09584540C250B70A0A9F7C54AC3B1C554CF5FD58AA99EBF3C50A67AE2AA67AE14A9B6C7E787985536EBF3C42A9375C2154C8A1C2AA6450E2AA645315533B1C55488ED8AA991DB155323B62AA67A6155323EFC55488AE2AA6462AB08DFE78AA9914FA7155261DB1552230A792991DF0D2A991DF1E695323BFDF88553618AA9118554C8C554C8FBB1553236DF155223B1C5216114C6D2148F7C8A4291EE7B62953618AA9115C0AB0F81C556115C0AA6457E78A5488AE2AB08EC714AC2298AAC2BE18AAC23155BC69EF8136D53DB0ABB88F0C16A1B03DB01A4DB7C4E042F0B8AAA2AE2AACABED8121582E295655C5510AB82D2022513025171A629474699125348E8D3DB025318A3E99025900BF5AFDDDB69769D0B2C97728F791B827FC2A57E9C38F724B1C9B003E29446996352656EEB691DCEA0E3E1D3E169941EF26CB10FA5C8C077DBBD236DDE7414B12CC4B331AB31EE4F53990D28944C0846C30B3BA471A9677215147524EC0624ABB56911AE12CE160D069EA620C3A3C95ACAFF4B6C3D80C603AF7AC9048993608845C0A8A44C0828954C508C863842CD71752FD5ECAD2369EFAE4FF00BAE25EA47893D14772464494C459A788F9835A9B5FD4E5BF913D180011585A56A21813EC27CFBB1EE49397C63C229CB02824B924BB15762AB5B084859812DE2C4AC63DBC31485B8A569C42ADC5563625569E980A54F02AC277C54AD6E98B20A6DD3150B3A0AE44A56602AB4E04953AD07CC62953E94AE285338A54CF4C554CE04858DDB14F5536C09536C8A549BAE2AA47AE2A5AC149A7FFD63C5FE19E86F9B2253B6042BAE283C910BFC3141E689538B1440ED8AA213F86042257AE28442F5C54A257031442E4555D715575ED8AAB0E98A159715565ED8AAB2E2BD55D770315561DC629555ED8155875C55557BE3D555476C015557B601C955875F9E3D15506290AABDF02AA2E14AA2E2AA8BD315545E98AAA29FC3155C3032551895545E98855EB802578DB12ABB155DD302486F9E2B6DF2F6C696D6E480480D6152B49EDF7E44962B7249256B786285B8A14F1569BA62AB31553C556B62AB0E108EAB0F4C4AF559DB0A9598A9587AE28536EB8AAC6ED8AA9B76C556374C2AA4DD310AA6DB8C239AA99E9F2C2AA67A615536E98AA99E98AA9374C554DBC7155323BE2AA6D8AA991DF0AA9918AA991DF155323AFE18AA9D3F0C554E9DBBF6C554CEFF462A148F8E14A9B7FB78F250A6464954C8DFE7812A64530AA991DB155223B62AA6462AA67F562AA6771EF8AA9115C52A67A1C8A79A91C5214C8EA30254DB6AFEBC55488EF8AAC23FB7155847DF810A647DF8A6D4C8FBF14A9915F9E2AB08F1C52B48F0C556915C556F1C0AD50E36975302B61702AEE38AAF03DB142A85C52ACAB8121595714ABAAE025404422FB60648B44E98A5191A74DB224A423A34C094C628FA640964995BC0F2C91C518ABCAC11078B31A0FC70134C846D05ADBACFABDE7A66B0DB30B680FF91001183F4F1AE4F18A886BCA6E469091A7B64896B50F30CBF56D36CAC47F797EE6EAE07FC551D5221F4B723F40C38C5927B913D8577B1154CB9A512898AA6F6E7EA36B3EA3D245FDC58FFC6671BB7FB05DFE64644EE692C79132C608945E98A11489ED810512A9ED8A115144CEC888A5DDC8544515249D8003DF05AB04F3F6BAA1BFC2F6120782CE40FAD5C21A89AE97611023AA43D3DDAA7B0C9628DFA8FC1C9C70E10F34CB9B1D8ABB15762AA67A9C290D604B7D3162A471641D8AAC277C55AC554F02563602AB4ED8A429E285A4D7166A6D8A858DD29910AB4E055327F1C0AB1B164A6715536C554DB0254C9DFE58A42C3B9C54299EB914A91C095327BE2AA58AACE47164FF00FFD73C5CF427CD95D7B6142257AE054426160885FE1810885C551098108A4C50AE9D29E0702944A7F0C58944276C05510BDF021597B62AACBFAF15565ED8AAB83DB15565C5559715565ED8A555761F4E2AAC0EFF003C0AAC3AE2AA8B8F55561D300E6AAB5E8702AA8EB8A41541D702AA0EB8AAA2E14AA2E2AA8BD698AAA2F5C55774DF1656A80F4F6C55501DF02AFC48554C414B75A7CB010AB81C4156F24905D8A6DD8B12D134E9915016610CB93B10C5661568F4C50A78AAD6C556E2AA78AAD387A2AC3885587A625056E152A7D0E15E8B0EC702858D8A029B74C290B1BA60429B6E308553EA298F5553F6C91553C2AA78A4A99F0C50A78AA991DB1553C52A646285322B852A6462853EA287155322B8AAC229BF618AA9914DC76C5549877C542991F8E1E695323F1C42A991921B2A991812A4461558476C554C8ED8AA9115C554C8C554D87E18AA9118AA930C09536F1C09F352618A7CD4D87DDDB14A9918154E9D46255652980AAC23BE2AA6477C536A645715585715594C52B4AF862AB69ED815AA62AD5305AB7C7DB02B7C7155E1715540BFEDE055555C521595705B25555C0A8845C59008A44F6C528B8D3224A691B1A74DB02404C228FA1C892C82611274C8B2013FD2A96F24F7EC3E1D32DE4BA1FEBA8E310FA6465C84F7DBBDB23B59EE61B1A13BB1A93B93E39916E25269676AD733C36E9B34CE1391ED5EA4FB01BE449A16902CD30ED6AF1751D52EAE62FF007995843663C2188048FEF02BF4E5F08F0C5A672B28154F6C93046DBC0F3491C312979656091A0EEC4D00C04D2B7ABCD1BDC259DBB87B5D3818A371D1E4AD6593FD936C3D80C6036BEF449008B93628A44C08B44A262844A274DB02A13CC5ADFF008574A49A16035FD5E361A4AFED5B406AAF7647627758FDEADD8608C78CD741CFF536E28752F051F3AFB9CC92E43B15762AEC55A3D0E2AA784B26F020AD6E98A8598A5A3B62AB31568F4C55660E49533B9C0AB49ED8A4AC3B62A14F14959DF014AC3B9C0792AD272295327150A64F7C54A993DF14A99EB8AAC3D70254CFEBC52A7D2B80A42C27015513D30254CF4C554DBA604859B6452FFFD03B5ED9E86F9B22177A62844A75FA0605564FE385814526042BAF6C55129DB02110BDB142217B62A8943D3EEC0C4A217015442E05565E98A3A2B29C555D7B62AACBD7155653B57150ACB8A5594F4C5559715551815541DF1E8AAA9FC30155553DB13CD5546E3E58F555507618121541E98AAA03B838142A7438542A0C52A98AAA03E18AAE076AE295E3025506F89554070AAE06982957E3CD2EC695BAD3052D3B962B4EA9C40480D61A505D8504DAD26B8A85B8A95A4F6C556E2858714AD3D3142CC5561EF87A2AD3885587A612AB3141587A9C2AB1BAE050B1BA62A16374C50A67A1C2AB302952192292A7DF0F352A67A9C2AB0F538AA99EB8AA99EB8AA99EB8AA9B0DEB8AA99EB85561EBFC715533D4FBE2AA646F8A1652B5C554CF707B62AA74EBF762AA6475FC30A5488EBE3895532324AA67155361DF14A991DF155361DF15536F1C554C8EF8AA9118AA99D8E2AA44602AA6475180A42911D4629E454C8ED8A7929B0DB0254CF4AE2AA6477C0AB08C556118AAC618142991E1F7614AC209C0BC94C8C52B4A9C55D4383656A8702BA87156F8E2AB82FB7D38AAF0B80AAA85F6DF02AAAAE0641555714ABAAE2C822113A62945A264494846469D3024047C51FB6449668F8D3229A4C224E981980995F7FA2F97F88349357BA083DE1B51C9BE82EEBF7608EF3F77E95C86A1EFFD0C6A38F2D7151B7137E8ED1F50BE078CF703EA163E3CE61FBD61FEAC751FEC8601EA901F1493C3127E0F3D44F6CCA71912A9ED8AA6D0B7E8EB09B50E97371CAD74DF10C47EF65FF60A683DCFB6408E234BC85B1C441B0CB582291302114898A114898A0A34CB63A658DD6B9AB57F45E9FC41814D1EEA76A98EDA33E2F4AB1FD95A9F0C81B268733F8B678E1C45F3F6B3ABDEEBDA9DDEADA8387BABB6A9551448D147148D076545015478665C20222839696E255D815D8ABB1558C7B61090B7025BC58A99EB8A4358A569F0C556E2AB0F5C55631DB014ACC0AB0F5C549B58DE18B301613B7CF1405BEF914A9E03BAAC38125613518A4299E98AA9B74C554CE04A9123EFC556B1ED8A563604A936455498E29536C55489DE9E180A5AC5777FFFD13B5ED9E86F9B2BAE284421E981510B8B1A442FF1C58A217F8E2A884C082895FE38A1129FD98AA217031442E05442F6C0A5597AE28565ED8AAB2E2AAE3A8C56D557155753F8E29555C555C1E9EF8AAAAE2AAA0ED815597B1C1D155076C4F25555EBF3C4AAAAF7180AAAAE252BC74A786055406A314AA03B7CB0AAA29C557AF862AA80D3155D5EF8A42A835F9F6C55703DC604AF070AAEC042B61BC7155D855D8ABB156AA3E78A56927156B021A27EFC2AB3155A4F6C52B7155AD8A16E2AB3245561EB8855A7A615598A0AC3D4E2958DD70202C6E8315587A1C57AA9F63854A98C4A953E872417A299EB885E8B1BAE15536EA315536ED8A858DD8E2A14D86DF2C5429B74AE10AA67A57C3155A4571472523BE2AB0EC7E58AAC7EC715A5323F6B14A99DE87085523E3E3D310AB08C34AA44614A9914DB142C228714A9918AA9914C55488EA3155323155223AE2AA6702A911518154C8FBF0242991DF148DD4CE29523D7F5E255630A1F6C09532287DB142C2302561C554C8A74FA715532BF4FB614AD23E8C095A40F9E3CD569182D5AA62AEE3815DC7155C062ABC2D705AAA2A9FF006F22AAA17E9C5202A05C592B2AE29015D131648B44C8DA422E34F6C0902D1D1C7D2B91259A3A38FA6D812023E28FA6D81980984519EC2BE03224B256F337C3A8C3A6AEEBA35BC76AF4E9EB1FDE4E7FE0DC8FA31C3F4DF7B56A0FAB87BBF05278D09A002A7C32C25A692BF364D4BDB7D210D62D1A3F4E7A74375251A73FEC4D13FD8E4F08DB8BBFEEE8C739DF87BBEFEAC6D532EB694C2CED24BBB886DA2A0795A9CCFD95037666F00A2A4FB644CA85AD5A1755BB8EF6EC0B6A8B1B4416F60A7A98D4FDB23C5D8963F3C9423437E6C646CA1513A649814522628452274C5532B1B36BB94A7A896F0C48D35DDDCA691410C639492C87B2A8DFF0EA721295058C4C8D3C6BCEDE695F315F456FA78783CBFA4F28B47B77D99F953D4B9947FBF2522A7F94517B65F8B1F08B3CCB9918F08A0C2F2E4B7905762AEC55C76C5548E164EC082E268314053C593B1553C55A3B0C55662AB0F5C8A5613B629A598162B0EF8591533B9C0A16B7860558DD322A16138A429B1FC314A99C554C9C55613812B0FEAC5214BC7150B09C8A548F5C095338AA960553ED8A5654F8E415FFD2394ED9E84F9B221714221715575C284427F0C58A254E042213020A217142254E2A8853810510BFC30142BAE054429E9EF8A1594E2AAEA7F1C55557F562AACBBE2AAC3A62A155714AB2E2AACA771BF5C5555702AAA9C555478E00AAA3B7B63E4AAA0EE0E00AAA0D314D2A0D8D71554069F4E295E3638AAA034C5552B8AAFC55703D8E295C0EF8A5501F0EB8A392F0714AE0DE38AAEC55D8AB7BF8E296B15762AEC50B4B786295B8AB89A62853C55D8AA9E2AD1F0C2156E2554F0AAD6C2AB71553C5561EB8A02D6ED81561E870A958312A54BBE48849533D4E211D16375C2A14DBB62A14DBB62958DD3E58A14CF4C5567638A9533BD70A565314153DFA62A561E9B77C5405A46D8A1469B7BE29586B4A6155322A31553230F35533842AC236F962AA6D8A54C8C554C8DB155322A3155323BE2AA6D8AA930EF8AA930C8AA91EB8AA99EBF3C0953229F2C52A6450E295323A8C0958476C50A67C302AC2298AA9918A56918AA995EBE38A792C229D46295B4EF80AB88C0AB78E2AEE38AB6147CF155E17236AA81702AA05C540550B819D2AAAE14ABA262C91289D3216A022913164023638FC46449668E8D322C8047471F4DB1640261147ED912595324D121885EADCDC2D6D74D8DEFAE94F74B71CC2FF00B26017E9CAB272A1CCECD98C006CF21BB0E7796E669AE663CA6B891A599BC5DC9663F79CC8AA14E11366CA6B62D1D8C777AC4CA1A2D222F5D11BA3CEC78C09F4B904FB039190BA88EACA2786E47A7DFD1E664C92BBCB2B1925958BCAE772CCC6A49F99CCB70C9B5754C5099DC37E8CD2A836BED690AAF8C7660FC47D8CAC283FC907C7223D52F21F7A49A1EF6388996B55A2D13142291314128EB6B796E258ADE089A69E7758E0850559DD8D0281E24E449A5E6C23F30BCCD0DBC52F93746B859A18E407CCFA9446AB737119A8B68D8758A16EA46CEFBF40B92C30B3C67E1FAFE2E5E38708791665363791250EC0AEC55D8AAC6385216E04B78B158C6A7E58A405B8A56B1ED8AADC556135C52B49A602AA781561EB8A6D6938AC54C9C53CCACC894ACF7C05569EF81561EF8A5489C52B0E2AA78AA993DF02561C52B0E02952380AA91E9812A64ED8154C9A614852272255AC8ABFFFD3385CF427CD910A7A6142BAFBE05442E2A884C582214E2844262A512870314427BE2A884ED4C50885C05088539155753F86285653F8E2AACA7A62AAEB8AAAA9DBE58AF45618AAB03F86295653BFCF1555538AAA83D302AB038AAA034C4AAAAFEBC055554F6C4AAA2EFF00463C92AA3714F0C055501A8F962ABC1DB14AF07B62AA80F6C55501A7CB155DD71482B8376C5697834F7C52BC362AB81C56DBE98AB7538AB7CBDB15772C55DC8E2AB715762AD138AADC55AC556135C55AC55664956938156E15587AE2AB4F4C2AB31553C556B62AB1BA610AB060282A59349587A9C42858DD7150A6DDB1558DD315EAA67A1C548598A959EF84AA9E2AA671558462B6B7A57141533FAF152B08A62AA646F842A99EF842A99C554CE129584531552E98AAC3D715533B1C2AA646F8154CF538AA911DB15533DC602AA447518154CF4C55488DB0320B0EE0E290A647DE3AE050A6702AC23BE2AA64571558462AA6462AB08A7CB1295A77EF4C16959C4F7DF02DB45714ADE2315771F0AE2ADD3DB155E17FDA19055E17DB15540BED8AAA05C0C80550B8A55513A636C912A9ED91B4844A262C91B1C7D3224B2029191A74C0C8047471FB606602611C7D3224B308F8D3A6D914A73747EA3E5B98F49F5EB816F1F8FD5AD6924A7E4D2141F41C8C779FBBF4AE43C38FFAC7EC0C5238F6AE5EE22979AE6FAA5AE9DA0A1A4945D43541FF00164ABFB88CFF00A919E5F36C3845932F8046734047E27F47D8C3513321C64DF4DB38A692496E894B0B2433DFC83AFA6A68157FCA7242AFB9C848D72E6542477D772EA57B35ECC02B4C470897ECC68A38A22FB2A80065B18F08A61295EEB5130B04522F4C56D14A800AE063682F38F988F932C5F49B1938F9BF568297932FDAD2ECE65FB23C2E2653BF7443FCCDB4211F10DFF08FB4FEA0E562C7C3B9E6F9F00A6DE199CDCBB224A1D815D8ABB15689A0C554F0B26F020B44D062853C593B1553C55A2698AACC556135C8A56134C55662A161C59F253277F96050B5BA530055B81561C09E6A6DDB148533D6BE38A853638AA993DB02561F0C542993BE2958C7B6452A4C70254DBAE2AA67AE2AA44EFF002C052A75EF912AD546297FFFD436539E84F9B2217142BAFEBC5510B8A0A214E2C4A214E284429C55108703144A9C5510876C54A214FE3818ABA9C8AA21715565E9F2C50AEA715555EDED8AAB29FC715561D31556538A5554FDE3155607BE2AAABD4F862AAAA7B786055518AAA83839AAA83D0E3CD5501EF839A855069852BC1EF81552BDF155E0E295406B8AAF07155C0D3E58AAF14C536DD7C714854AF5A62A1B07155C0E2ADF2C55BA8C55BA8F1C55AA8C55DCB155B53E38AB58AB89A62AB09AE2AD62AB49AE10AD612AB0EF880AB49A615598AAC3D7155A7A62AB315587A9C556376C31559D8E279A953C92AC3D4E2A14DBAE2AB1BB62AB0F4C55662953C2AB0E28594A62A56118AAD3D714158C37F9E2AA47C70A8587B1C42853230D242991850B08C52A6D8AA9B0EF8AAC618AA930DABE1855611518AA9B74C0AA4C3BE2AA6C30155261DF0254DB15523D7142C61F8E064A6763F8E04A9918AAC2315532298156118AAC22B8AAC23B1C52B48C16AB283C3025695F7C56DAA1F9E2BB3A87C3156F8E056C2E455785C0ABC2E155E177C0C80550B8B25554C04B2442A74C1690110898B245C71E44964023634C8B208D8E3F6C5B0047C71F4C892CA91D1C7D32294C61859D951139C8E42A20EA49D801F338095015FCD0E87555D3616E76FA142B608C3A34A84B5C38FF005A566FA298E11E9BEFDFF531D41F550FE1DBF5FDA84D321B75925BDBD5AE9FA5C4D777C3F991281631EF239083E792913C8733B35C00BB3C86FF008F7BCE2EEEAE351BCBAD42E9B9DCDECAD34E474E4E6B41EC3A0F6CCB8C44450E41C1948C89279969232485505998D1540A924F6186D8A3B5B9058C29A0C241789C4DACC8370D714F862AF710834FF589F6C18C5FABE5F8F3599A14C7D532D6A44A27B62A8B44E9818A2357D6ADBC91A5DBEB1711C771E60D450B795B4A94065500D3EBF7087AC6847EED4FDB615FB2A6B5D788784721CFF57EBEE723163FE22F9BEE2E6E2F6E2E2F2F2792EAEEEE469AEAE6562CF248E6ACECC77249353996001B06F51C2AEC55D8ABB15762AA64D70B20D6056FA62C54C9A9C59358AAD27B62156E2AB09A9C52B58D301553C0A1613538A485A4D3E78A40584D37C579ACF7C894ACEA6B895584E029584E0485327738AAC26B8AA95715587AE04A9D7BE295980A4299C0AA44F7C095327A9C554C9A0C5548EC3004AC3B0C8AA9E3697FFFD53453D33D09F364429C555D4FE18508853DF02A214F4FBB0B128853D302110A7142210F4C08288538A112A7FA62A885381055D4E042214E055707142AA9E98AABA9ED8AAAA9FC315561D7E78AAB29C55581C52AAA6B518AAA8EDED8AAA83D3DF15550702AA034C55554FBE03B2AAA9ED80EC55501ED8521501EC70155E0D3E58AAF0698A5783DC62AA80D7155E0D7155C0D3155E083FD3155C4E2CADB07B8C5575714375C536EAE2B6DD462AEA8F1C55D51E38AB5518AB5CB155B8ABAB8AADAE48056B12556135C695AC2AB09AE2AB49A605598556B75C5569D862AA784AAC6EB842AC3D31EAAB30A54F142C3D4E2AB1BB6295B8AA9F8E2C42CA530949533D710AB5BA1C2AB30295A7140536EB8A8587085536C3E6AB0F4C2AA67A6295322A3155877C554FAE2AA78AA9D29855611D4605523E18AA99EE3155223B64554C8EB8A54C8C0A54CEE31482A646C714D29915C0AB08C0AA64571558476C556114C556118DAAC23C72295A57155B4F6C556F118A5DC705ADB7C7E780AAE0BED815B0B8AAF0B812BC2E2901542E36C95157224A510898134AEA9859808B8E3E990259045A26064023238FA62D8023E38E99125923A34F6C8A51D1C7D36C04A6993E8616CE4BBD6645063D0EDDAE901E8D70484B65FA64607E40E55937F4F7FE0B663F4DCBF9BBFC7A7DAC2D559897725998F2773D493B927E7992E135E689FF47E9765A121A5CEA1C351D5BC563A1FAAC47E826423DD7C31C238899776C3F4B1CF2E1888F53B9FD1FAD832AE655B889EDA32E9366DAE4801B8E4D0E8911FDA9C0F8A623F96106A3FCAA7BE572F59E1F9FE3CD37C238BE4C442924B312CCC49663B924F524E643492AE89B8C505168B818A613DD699E5AD27FC4DAF47F58B62ED1687A2D78BEA5729D56A37586324195FFD82FC476AC9333C31E7D4F77EDEE6EC58EF73CBEF7CE9AD6B3A9798B55BCD6B57B8FAD5FDF3F29A4A7155007148E351B2A2280AAA36005332A311014393944A599243B15762AEC55D8AAC27B61E49016E04B78B158C7B629016E29689A62AB315689A62AB305AA9935C095A4D315598A42C26B8B2254C9AE2A02D6F0C8F9AAD3B0C0AB09C090A6C714AC3B0A62AA671553276C095327B62A161F0C52B1B2295238154DB14A937862AA6C7B6048533D722554DBAFCB156B02BFFD632539E84F9BA214E282AEA70A1109DB02A214E163DE884381088538A15D4FE38A0A254FE38108853D3154429C5055D4F4C0508843FD322AAEA7F0C50AEA7F1C55557A7CB155653F8E2AAAA7B62AAC0E2AACA714AA8EC7155507F1C55554F6C55554F6C0AA83155507B62AAA0D7E78392AA035C55501AFCF0724AA035C792AF07B1C557834C52BC1EF8AAF06B8AAE07155C0E2ABAB8AB78B20BC1AE2ADD715B762BB3B15762AEC55D8AB5503156AB84056B0F25689C16AB49AE1015AC2AB09AE2AD62AB09AE2AD62AA78AAD63DB080AB702A91DF2612B5B15598AACC50B0F538AAC3D314ADC5561C5885870A958C314ACEB8AA9E2A169FE38A029B0EF8A561E9850566212B324AA78AA9E2AB0EC715533D7E78554DBAE0558D8AA9375C556360B5526EB80AA9B0C52A47AE28533D7024058450E0654A676C54A991BED8AAC2302AC2315584571558712AB08C1695B4C0AB78E0577138AB5C715771C16ADF13812B82E055E1714AE0BED8142F0BDF166052AAA60485654A60485754C6D9808A44C892C8045A26066022E38F7C59808F8E3F6C892C9191C7D322CA91F1C7D3012947C71E45537D64FD4743D334B1B4FAABFE93BEF1112F28ED54FCFE37FA46471EF227BB6FD69CC786023DFBFEA4A74BB6B6324D7B7E0FE8CD2E26BBD469B728D0802307C6472107CF2C993C8733B068801CCF21B9FC79BCDF50BDB9D5B50BCD4AEC86B9BD95A5969D054ECABE014500F6CCB8C444003A3813999C8C8F3289D374F37D71E9B482DEDE2469AF6E9BECC30A6EF21F90D80EE68304A54802D2ED5F501AA5E7A914660B1B6416FA6DA9DFD3813EC83E2CC49663DC9396E38708F36B9CACA015326C1128981894DA31A6E97A6DC798FCC323C1A0D8BFA4B14642CF7F734E4B696D5FDA23776E88BB9DE80D729127863CCFD9E67F1BB6E2C7C5B9E4F9F3CCFE66D4BCDBAB49AB6A5C22A22C1A7E9F0D441696C9FDDC10A9E8ABE3D58D58EE73271E318C50FED7298FE4D5D8ABB15762AEC55693DB084859812DE28256934DBBE2A16629762AB09AE2AD1C554CEE70256B1ED80AACC5202C26B8A1693DB164029934C0BD567BE02959EE712AB49C8A56E29523BEF8AAC3DF155238AAC277C0959E2714A9E0290A64F7C1D154C9C09533E38AA913DF02A993D4E29599155327BE0559F0F8E157FFFD73053D0E7A13E6E88538A15D4F4C284421C0AAEA7A7B6154429C0C110A7F0C50AEA71544A9FECC084429FEB8A15D0E2A8953818ABA9C0AAEA7BE05575C50ACA7F1C55554F6C55594F7FBF15F35507155607150AA0FDC714AAA9EDF762AAA0FE18AAA838AAA03815541E94C55501EE3155407B8C0AA80F718AAA035C09E4BC1AE3C957834C52BC1C557838AAEE5E38AAFAFDD8AAE07155C1A9B6D8AAEA83FD7156C629B762BB3AA715D9D538A69D5C569D5C36AD636AEA8C0AB6B8695AC92BAB4C556135C0AD62AB09AE156B15584D7156BA62AA75A9C3D156B1A6D880AB3249587AE285A7A629598A02C3D7155A7A1C55662AB3B615587152B4F7C554F155ADD714058DD315EAA67A61495B92553C792A99C5561EB8AAC6EBF3C554DBB1C2154D86DF2C55611B6055338AA99E981548EE302A99E98AA9B74C09536E98AA991DF1480B08EF812B08A8C0AA6462AB08ED8AAC231B5584605584604AD2B8AADA788C55AA0C0AD711895771C8DAB617DB14AEE38AAF0BED82D5705C095C130330AAA98A559530255950F8624B2011291F4C8B3011489819808B8E3C0CC047471D29B60259008C8D2B916548E8E3E9B602528F8E3E99154F747D3BF48DFDAD9B3FA514AC4DCCE7A470A02F2B9FF5501395CE7C22DB31C388D253ABDF8D5F55BDD442FA50CCF4B587FDF704602429FEC5140CB611E0880E3E59F1C89FC79259E6DB8FD1DA7D9F9723DAE6E4C7A86BBE2095ADADB9FF005118BB0F161E193C0388997C07E92D3A9970C443AF33FA07E9606919620282CCC400A054927A0A6653849AEB720D2ED7FC3D01ADCBB2CBE60954D7F78BBA5B03E11756F17FF57238C711E23F0FD6B90F08E1F9FEA630A9D3321A55D5302094DADADEC2DAC6EF5ED76E5B4FF2EE9842DE5D200659E56154B5B553F6E6929B0E8A2ACD4032B9C8DF0C7727F167C99E3C7C47C9E0FE70F375FF009C3528EEA7896C34CB1436FA16890B130D9DBD6BC413F69D8FC5239DD9B7E940323163101DE4F33DEE5F90627962BB15762AEC55D8AAD27B62901662976282E2477C50A78B20EC55693DB155B8AAC27155A4D0602AA7812B49ED8AADC52029E292B0EE70256B1ED802AD2694C8AA99F6C53CD63138A42962AB09C5561341812A67B62A16378629E6B18E452A4D8154D8E29536F0C5549BC305A429938156378605533E1812B30ADBFFD01CA7A7B67A13E6EAEA7F1C50885C50AEA7A7BE2A8853D30AABA9E98B128853FD302110A7A6285743810512A7F0C5088538AABA9C084429C4A15D4E45510A7142AAF862AACA7155507F1C55594E2AAA0E2956069B62AA80FE18AAA83DFF000C55541FC715540698AAA83BE055E0E155507C302AA03DF1215501EE30257835C0ABC1F1C557834C52B81C557838AAE07155D5DB155C0FDDE18AB60EF8AAF0698ABB91F1C52D72F1FD78AD3751E2715A7570D2D3AA71A4BB0D2B58ABAA3C7155A5BC30AADC0AEAD315584D70AB58AAC26BF2C55AC556135F9648056BA603BAA913535C900968F4C50B3155AC7155B8AA9935C5569C2AB702566142C38AADC54AC3D7150B1B085587A605587A610AB30AA9E15587AE2AB1BB62AA6DD3085587A605533D0E2AB0F438AA9E02AA4702A9FB604A9E155323A8C08533B8C52B3A8C0C96605584531558462AB08FBF01553380A5610715688C556F1F0C55AA1C0AEE3815DC4E0577138AAE0BED82D21705C16ABB8FCF025705DFA62C8055098B25554C095554C04B2011291F4C0CC044AA7B6066022E38F033011B1C7D3224B301171C781923A38FA6D912551D1C7ED813C28E8E3F6C892C80A6424FE8CF2EDEDD0DAEB5D73A759F88B74A3DD38F9FC11FD2D901EA981DDBFEA6523C18C9EB2DBE1D7F5247A6476D6A977AD6A118934ED1504D2C2DB09E663482DFFE7A3F5FF2431CB6649A88E67F04B8F0A8DCA5C87DFD03CC2EAE6E750BCBABFBC90CD777B2BCD7329FDA773527F1CCC8C44450E41D6CA464493CCA716CCBA1582EB7201FA42E4B47E5F858568CBB3DD91E11F44F17FF0054E448E33C3D3AFEA507847175E9FAD8680CCC5998B3B125D89A924EE493DEB993C9C75654C16829B5B5AD9436577AE6B97674CF2EE96545FDF280D249236E96D6C869EA4D25361D00F89A8A321291BE11B93F8B3E4CF1E3E3F73C2BCE7E72BDF385F42CD00D3344D30345A0E8313168AD6263F13336DEA4B252B2487763E0A0017E2C5C03BC9E65CBE5B061D96ABB15762AEC55D8AAD27B61A480B3025D8A92DD682B8B15326B8B26B155A4E2AB7155A4D3E78AADC095326B8156934C535B2CC5016138B3E4B09EDF7E280B49A0AE479A567CF0136AB4E05584FDD8A4293135C52B0E2AA6715584FE1812B09EF8A54EB80A853270254C9C095327BE2AA44F738AA993DF024ACAE44AA9E295327AE056A87C7157FFD118A7FB73D09F3757538AABA9C508853850AEA7B6054429C50AEA71628853D3142BA9C5510A70314429C5510A76FD78A0ABA9AFD3810AEA7052A254ED815541C50ACA7155553DB15F25553F78C55581DFF0C55554E295507EF18AAA83E1DF1555534F976C55501A6D8AAA038AAA83E38157834C2AA80E2AA80F8604AF0707355E0D7E78AAF071A55D812BEBFEDE2AB81F0C5577218AAEAFBE2ADD7DF155F538AB75C55D5031A56F90C6934EE431A56AA31A5772F0C34B4D54E1A4B58ADBAA3145AD27C314ADC2AE2698156135C2AD62AB09AF4C2052B5892AA64D7080AD614AC26B8AB58AAC3B9C50B09C556E2AB5B0AADC52B0E280B4E2AB315587AE150B0E21561E98D2ADC2154F12AB0F5C2AB1B1558DD315533B8C55662AA78AA9E02AA67A9C0554CF538AA99EA715533D4E05533DF14ACF1C05214C8A1F9E2A02D2302561151895598156118156915C52B298AB54F6C0AB48FA3015771F7C16AEE38AB7C7DB155C17E8C8DAAEE3ED82D2177138A5B0A716402A04C0C95427B60B4AAAA6064022523E869819808844F6C59808B48FDB033011691FB644966023123E9809A648D8E3F6C8DA691B1C7ED812023638F22C935B3B39AEE782D6DD3D4B8B991628231DDDCD147DE7232200B2900934157CC73C777AA258581F5ECB4845D3F4FE02BEA9427D4900F19652CDF2231C42A36799DD8673C52A1C86C3F1E6589F9CEF16DDADBCAD6AE1E2D21CCBABC8BB89750714715EE205FDD8F7E47BE6469E37EB3D797BBF6F370F573AAC63A73FEB7ECE5F363BA5E9F04E67BCBF768749D3544BA8CCBF6883B2431FF9721D97C373D065D3911B0E65C4880773C8247A9EA13EAF7B25E4C8B0A90B1DB5AA7D88214148E24F651F7F5EF97638880A6A9CCC8DA15530B5A6D6F6D636D6173AFEBF76DA5F9734F6097576A034D3CC4556D6D10D3D499C76E8A3E26A0C84A46F863B93F8B3E4D98F1F1FB9E11E73F3A5FF009C2F60AC0BA6685A6068F41D02262D15B46DF69998D3D4964A032487763E0A001918B1080EF279972B61B061B96ABB15762AEC55D8AAC26B85202DC09762A4B78B15326BF2C5900D62AD138AACC55A2698AACC556B1ED9129598AAC26B8AAD276C5900B09A62BCD664494AC26A6B8F255A4E05584E0480B09AE29533D7150A44E2AB18F6C0AB0F87DF8A429938A56B1ED914A9138154D8F6C52A6C7B62AA4C70148584EF4C0AA6C7B64554CF878E29595DF156B90C6D69FFD212B9E86F9BABA9E98155D4E284429E98A15D4E2A8853F7E282AE870B128853DF0215D4FE1BE2A884381055D4E284429ED8AABA9A628215D4E44A1128702AB03DF142B29EDE38AAAA9FC3155553DFEFC55594F6C55554FE18AAA29DF14AA83D3155507EE38AAA823EEC55501ED8AAA038AAA038AAF0698AAA038AAF07BE295E0D702AF07155E0E34AB81F1C55757C0E0A4AE0DE3802AE07C30D2B75C0ABAA0FB62ABB91C34AEA9F1A60A56FE9AE1DD5D518EEAEE430A69DCBE9C569AE4715A68938AD358536E269D7022D6F2F0C2AB714B44D310156124E1D82BB02AC2D5F9648055B8556938A56E28584FDD8AB58AAC26B8A5AFE1850B09A8C51D5A38A5662A16375C556E2A5664956375C8AAD3D326AB302A9E15587AE2AB1BB62AB0F438AACED84AA9E0553C4AA99EB912AA67A9C554CF5C554DBAE29587AE0480A646F8142C6D8E2AD52B812A74C5561182D569DF0256114C556900E2AB48A602AD53FCF7C16AEE3FE7BE05771FF003DF156C27B644955C1705AAE0BBFB629A5DC7E78A5B09E0305B2015026D82D905E130320ACA95C59008848F0320110A9ED819808948FA624B6008C48FDB22C8045C7174DB224B2011A91E459808C8E3E982D51B1C7D322947471F4DB1564DA7B1D274DD435D3F0CEA0D868FE3F599D4F3907FC628AA7FD62B9548714847E27DCD913C1132F80F7FEC0C720B81E5ED2EE3CC6C07D6918DA7976322BCAF0AD5A6A785BA9E5FEB15CB88F125C1D3AFBBF6B8BC5E1478FAF21EFEFF87DF4F34B0B1B9D4AEE2B5B71EA5C5C313CDDB61D59DDD8F40055989ED99922222CBAC1132341AD7751B79843A4698C5B47D3998ACF4A1BAB8228F70C3DFA203D17DC9C962811B9E67ECF2639663E91C87DBE690AAF4CB1A53558B4DD334C93CC7E65B97B0D0207314422A7D6AFE7515FAB59AB6C5BA7373F0C6376DE8A6129127863B9FBBCCB663C7C5B9E4F06F38F9CB51F38DF453DCC49A7E97A7A18742D06DC9FABD9C24D485AEEEEE77791BE263D76A01958F10C63BC9E67BDC9E5C987658AEC55D8ABB155A5A98D2D2D26B859535815D8A2DBC50A64D7E58B201AC55A2698AACC55C76C554C9AE05689A624A54F0285A4F6C5242DAD315014F149584D70256B78601DEAB702AC2702A993DBF1C592CAF5FC31553271558712AA64F7C09587618A5613DF014A993DF02A993DF02A913DF14A993DCE2AA64F7C894959EF809553F7C5561C52A6DB6D8142CA8C536FFFD3594FF5CF437CE08442EF810ACA7A6284429FE98AABA9C28575C0AAEA70B12885381088538A1594F4C508953810AEA7155753B7BE2AAEA703144A9C0555D4FE3815554F6C50AC0FDF8AAA838AAA83FD98AAA838A5541FEDC55541FB8E2AA80FF61C55541F1C55501FEC38AAA038AAF07EEC55501A62AA80F86055E0F86155E0FDF815783E38A57838955C1B05AAE07C30AAEE58A5757DF155C1BC702AEA8F1C695D8D2B609F9E3BAAEE58DABAA31B57546157546296B90C569AE5ED8AD3552715A6B0ADBAA3C702AD2C7E58AD2DC29689031A5584D72402ADC2968B7862AB7142D269F3C52B71568F4C556628689ED85561C5569C556E155ADD7028587A6155B84AA9F5C42AD38855B8F5553C2AB0F5C5563610AB0F4C0AB0F438AA9E2AA7912AA67A9C52B1BAE2AA6DD70214CF518A42C6C0958477C556F5C0958453155A4572295876C556118AB445302AD22B8955B4C8DABB89ED8AB614E0B56F8EF80AAEE19155C13156F8FCB1B654B826025900A812982D900BC262C955537C590442C791B6402BAC78B3011491F4DB012D8022923E9B6459008B8E2F6C892CC046A47ED81922D23C16946471FB64551D1C7D36C6D2985BDBC92C91C512192595824518DCB331A003E67224D240B4E357B76D4356B1F2CD8489F56D195A092E49A45EB7F797B72E7F954822BFCA832103C31333D7F007E3BD394714840721F827F1DCF2DF33EAD1EB5A9C7169EAE347D31059E89090793460D4CA57F9E6725CFCC0ED99B871F0477E6773F8F2755A9CBE24FD3C86C3F1E7CD09ABCA342B39B42B760756BC503CC370A6BE8A6C459A91DFBCA477A2F6396631C6788F21CBF5FEA69C8780708E7D7F57EB61EA832F71933B87D1FCB7A5C5E62F353482CAE397E85D0E160977AA3A1A1F4EB5F4E053B3CC47F929C9BA409948F0C39F53D07EDF26DC78AF73CBEF7CFBE6AF35EADE6FD4FF00496AAE88B0A0834DD36DC70B5B2B706AB05BC7FB2A3A927763F13124E6563C6318A1FDAE43186CB15612062AEE430D2D345BC31A4D2DA9C534D605762ADE2825C4D31429935C5900D62AD134C556135C55D8AAC26B894AD26991559EF8AAD271559819456135C52B09ED8542D268323CD56E0255613812B09C52A6715532715584E2AB09AE04A99EB8A8584D7E8C52B18E452A64F6C0AA4C714A993DB15536C095327B63C9563761915533E1812B0E2AA4C6B8A54EB915A7FFD47A9CF437CE8A210E060AEA7F1C555D4E2857538A15D4F4C2AAEA7BFBE041575385894429C08575C56D5D5B020A214E2857538AABA9C084429C0508853815581C50AAA7BE2AAAA7F1C55541ED8AAA83F7E2AAA1BFB46295507F1C55501FECC55541F1FA3155407155406B4FC3155407B62ABC1F0C55783DC62AA80FDF815783E3855783E38AAE07C30257838AAEAE2AB8378E296C1F7C0AB831C6D57541F6C695BA9F1C55BE470AB75F9E2AD823143AB8A5D5F962AEE43156B97B62B4D7238A69AA9C55D8ADAD2C07BE102D5616272549A6B0DAB4481EF82D56135C29762AB6B8A16E2AD134C52B2B8A1D8556135C55AC554F1571DB1553C556B6156B24AA78AAD3885587A62AB315587A9C2AB1B02AC3FAF15533D3155982D54B02AC3D4E29536EB8AAC3D702AC381239AC61518A95B8A858453A7D382D792DC8964B08C55A23155A478602AB0EF82D5AE2302BB8D37AE2ABB8FB9C8DAB617FCCE055C1702AEE380956C2F88C0C9784F6C5900BC2FB606402F0B85952AAC7FED60B6402BAA606602BAA74C4B3011291FB646D9808A48FDB032A45C717B644966022D23E982D922D23E9B64494A3123E9B604A3638FA60546C71F4DB02593698DFA26CAF7CC0D412DA7FA36915EF792A9A38FF008C2957F9F1CAA7EB223F3F77ED6C89E0067DDCBDFF00B1877982ECE83A28D2E362BACF98A2593516AFC5069EC434711F06B8203B7F9007F31CC8C51E3971748F2F7FEC707513F0E1C23EA973F21FB7EE6248E3CB3650EA2C07E9EBF8F96896EC2A6D616DBEB8E0FED1E9103FEBFF002E64D7886BA0E7E7E5FADC1BF0C5F53CBCBCFF0057CD83D09ABBB1624F267635249EA493992E223B58D4749F225A457DE60B74D475FB98C4BA2F93D89078B0AA5C6A1C68638BBAC7B3C9FE4AFC595C6F29A8EC3A9FD5E7F737C3156F2F97EB7CE9AF6BDAB799754B9D675CBC7BED42EA81E5202AAA28A2471A2D151106CAAA00033321010141BAED25C9AAC3D71553277C21216E3697605762AEC56DBC58DB44D315A53C590762AD134C556135C55D8AAC26B8AAD269839256138156934C55662AB49EC3166052C2698A02DF73913BA54EB535C4F72B44E4556138A54C9C52B09ED8AA993DB15584D302A993DB14AC27B629587619129532712AA64D30254C9A62AA64D30279A9138AACE832369598AA9B1EB810A64ED4FBF1641498FDC30286B1B4BFFFD5CA7F0CF427CE95D4E2C55D4D47CB142BA9FC7142BA9C555D4F4C285756C0AAEA70B12AEA702110A7142B29C54A214FDF818ABA9C555D5BB0C5512AD8189575382955D5B02AAA9E87EFC50AA0FDD8AAB038AAA03F7E2AAA0F7C52AA0FDD8AAA03F762AA80FDD8AAA838AAA06EC7155E0E2AA80E2ABC1F0C55786C55786F1C557838157838A57D7C7155D5C55772C55BA8C52BABEF8AB75C695757DF156EA7156F96286EA3156EA3C71576D8A5AA8F1C569DC862B4D72F6C569AE4714D35B9EB855AC6D6D6961DB0D256924E1A56B0A5696C50B49AE2AEC55693B62AB71576155A4E28E6B714AD27155B8556938156E1559920AB58E2AB7005533BE155AD8AADC554FBE2AB0F538AAC6ED8AAC3D0E46D54F1553C5561EA702561EBF462A14CE050B48DB15A5A31641691DF05A16644A5A22B8A56914C5569180AACA0FF003382D5D4F9E05771F6FBF0155DC7E591B56C2E055DC705AAEE3ED815DC7E8C592A04C5900BC2FDF81900BC2636C95553DB032015D53FDAC5980AE91FB60259808958F22CC044A47ED80966022923E9B6459808B48FA6D91259522923C0946247ED8AA3238F02A3638BA60253498DB5B493CB141046649A6758E18D7AB331A281F339027A966059A09E6AF369F67CE5B9E373A079357D2F4C1A2EA1A9CA6AD1A9EE1DD684F6892B90C609E5F54BEC1F8FB4AE69C61B9FA61F6CBF1F60792B4FCCDDF9C7CCB4BD92F2776B1B27DBEBD743A8A0E9045B72A7B20F6CF039421B57D9FB4FED74F29DDE49EE4FDA7F57F630B964D435BD49A697D4BFD4F519400A8A59E491B654445FB8281B0D866500203B8071244C8D9DC94BBCC7E6CD3BC83CECF4E6B7D67CF2869249F0CF65A3B7BF549EE4786E919EBC9B6021039B9ED1FB4FEA1F696F863E1DCF3FBBF6BE77BDBBBAD42EAE2FAFAE65BDBDBC91A6BBBC9DCBC92C8C6ACCECD524939991000A1C99A0CF4C92ACC554C9EA71552C593B15762B6DE28B76285A5BC31480B314BB15689A62AB315760E6AB09C2AB49A62AB09AE452B49A62AB315689A62CC0532714735B8094AC26B8392B5815613813CD612294C52A44F5C5696138AAC27155327BE04AC2698A792CF7C0901613815489AE04A9935C554C9AE05584FE1894A9135C0556135FA302AD269812A64E2AA4C77AFDD8A54CE04A9E34AFFFD64D4FF4CF427CE910A71415753D316255D4E2AAEA7142BA9FC7142BA9C55594E282AEA70B13B2214E0557538A1594E042214FE38A1594E2A8956FEDC082AEA7142BAB602AAEA702AA83DB142A838AAA035DF155507BE295556C55501C55501C55501FECC55501A7BE2AA81BEEC55783DF155E0E2AA81BC7155E0E2AB81F0C55786FA302AF0C7BE295C0E2ABB962AB81C557023C7E58AB7C8E2ADF2C55B07E78AB7C8E296F96286EB8A5BA8F1C50EA8C296AA3C702BB90C569AE5ED8534B6AD8765D9A35386C2DB5D3A9C6D36B4B786156AA4E2AD62AD5462AB49C55AC5435518556D71B5E6D62AB49C556E15689C0AB30AAD27B6215AC92AC3BE2AB4F4C14AB30AAC27155A7618AACC554CEF8AAC6C05563605587618AA9E04A9E250B4F53812161E98AB58192D23C305A16602905A201C52B48FA7021A3D31296A99156A87B62AEE3EF91B55DC7E79142EE38DA5BE382D5705C0ADF0AE36C9784A60B6402FE18B201784AE064ACB1FF00B58B2015963E98096C015D63F6C04B30110B1E0B66022123C892C8045A47ED91259808A48FDB22CD1491FB62A8B48FDB05AA2D22E98B2011B1C5D36C892908E8E2F6C8A59668B6B71040F7F6CA0EA374E6C34104D02CEEB59AE093D1608C924F6241ED94CE409A3C86E7F57C5B600816399D87E93F00F3AD6AEB4ED448267923F2479598C514C9F0C9A8DDBEF23460FEDCC46C4FD88C027DF3714651FEBCBEC1FB3ED2EAB5192333FD08FDA7F6FD81E7D336A7E6DD4A499638ADE1B586A14B08ACB4FB38BA7391CF18E341D598D49DF763998047147F164BAF9196597E28079C79A3F31AD74C82E742F21DC3D67568757F3A1531DC5C29D9E1B153468213D0BFF78E3AF15F872C86132F54FE03F5F79FB03746223CBE6F103B6C050665A8533D714A99C2AB18F6F1C554DBB6290B315B6F15B7628689A62A16124E2CA9AC55D8AB44D315598ABBA62AB09AE2AB7A63C92B09AE455A2698AACC556934F9E2C80584F7C579AC3B9C04A5693DB02ADE981561C09584D314AC27BE2AA64E2AB09AE055326B8A561FD58A40584D7150B18F6C8A54C9ED812A64F6C554C9ED8AA9B1ED81214C9A634AB09C89553E98154C9FBCE14853AF6C095326BF46050A6C7025AC55FFD7414F4CF427CE95D4E2AAEA70B1A57535C085753F8F5C50AEA7155653850AEA7FB702ABA9C28A5756C58ABA9C0856538AABAB7F6E042214E285746C5510ADF760452BAB7F6E28565380855653F778E042A83FD98AAA86FA314AA83FDA3142A03D3C314AA038AAA86E98AAA038AAA03F762ABC1FBBC3155E1B155E0E2AA81BC7155E0E2AB81FA3155E1BC702AF07C0E155DCB0257838AB7CB156C118AAE048E87155DCBE9C55BA83ED8AB7538AB7C8E2ADD71577218ABAA315754614BB90C6969AE7ED878569AE67C31E14D35C89EF4C349D96E15B6AA062AD16C556F5C55D8A5A27143553F2C2AB6A715A774C56D6935C556E2AD134C556615689ED8AADC92AD27B62AB7015584D70AAD2698AACC5569C5561FD78AACC0554CEF8156375C5561E98AAC3D302A9D3014D2D3D7148775C09E6B0AF87D38DA296E44A6DC7B62B4B4AE02AB482060255AA57B57025AE35380AAE0BED82D6D705C8A1771C1695C1705AAEE3ED82D5B087C31B641784F6C16C805C13DB16602A04C534AAA982D980ACB1E0B6602BAC7ED81980AEB1FB60B6C011091FB646D9008B48FA6D80966022923C8B245247D31544A47812023238FA6D819008C8E2F6C892946A47D302A69616335F5D5BD9DB81EADC38442DB01E2CC7B051B93E19094A85B28C788D04CBCDB7905B5B4BA55B5D1D3ECA0B358F54D508F8AD34D90F208ABDEE2FDFE2E3D787153B72A3A7892788EE6F61DE7F547EF61ACCA223801ADB73DD1FD72FB9E17ACDF59DD5A45AD6B53FF863C93A672B7D1E0A7AB3CCC3778ED62AA99E773BC8FB2AFED3050066CE038361EA91E7F8E81D21BCBE511CBF1D4F7BC13CE3E7FBCF31C3FA1B4CB6FD03E548640F0E8D1BF37B875FB33DECC003349E029C13A228EB9978B0889B3BCBBFF533AA14393CE8F5F965C39AA9138554F0AA993FAF15584F5C554BDF149762868903155A5BC31480B714BB15762AB49F0C556E2AD134C556135C09689A636AB09AE05689A62AB09C54AD2698B201613E38136B2A49C4AAD269B0FBF079AADC1CD56935C095338AD2C2714AC2715532715584F6C0958715584F618A79AC2699129532712AA64F7C095327BE2AA64F7C554C9EE70734ACF7C8955326B8AAC634AE05584ED8A5498FDF894A993812B09C0AD5714D3FFFD008A73D09F3A5753FDB8AA214E28215D4E2C5594FE38AABA9C50ACA70A15D4FEAC0AAEA71556538B12AEAD8A15D4E2856534C555D5B02112A7142BAB62AACA7020ABA9C050ACAD815594FF662AAA0FDF8A15037E1D714AAAB7862AA80F7C55501FF006B155407FDAC55501C55501C55786FA3155FCBC7155E0E2ABC1C55706F1C55786FA7155C0E2ABC378E2ABABE07025772C55757156C1FF6F155C1BE9C55BAE2AEE58AAEAFBE2ADF2C55BE58ABB90C55DC861A4B5C863455DC87861A2B4D72F0186934D54E15D9ADF14DB58ADB551F3C556D4E296B1576286ABEF856DA27BE2AD62AD6155A4F86055B855A2698AADC202B44D30AACC55693DB155B8AAC3BE2AB49A62AB315584D715584E46D5662AA78AAD6C0AB4F7C095831641C460410B28460B5BA6B026DDD715A5A478644AD2D20F8E3696B02B7C7DB224AAE0B82D0B82FB602AB82E452BC26055DC7C71B4D37C305A405E1462CC05C1316402A04F6C04B201504782D980AEB1E06602B2A60B6602BAC782D9808848FDB05B20114917B646DB00452474A6D819008948F032A45247D36C528B8E2C892A8C48F22A8B48FA6D8A691691F4DB2297A168F691E89A05D798EF0224978AD6FA5ACC0B27A75A49232AFC4CA5A8BC5777FB0377CC799E39880E9CDBC118719C87AF2FC7E2F9757CD1F989E7BD1F44B87875456D63558A57B8B5F28B49D2E64DDAF35A9A33B4AFDA043C956884A0EBB7D3E1246DB0EFF00D11F2F3743965C67D5F2FD27CFC9F2E798BCC7ACF9A7503A9EB77A6EEE0288ADE35023860887D9860896891A2F6551F8EF9B084040506ABB2C7C9EB962548F4C215489C42AC270AA99E98AA931DA9E38A429F2A62BCDA2C715A5B8A5D8ABB156AB4C556935C55AC55A2698AACEB839A5A2698AAC26B815A2698A40587142D269F3C0C80584F8E2A56135FE98A40A5A4F6EF8156E055A4FF6E05587F0F0C521613BE2953C55613DB15584D302A993F79C52B49A6292B09A60252A64E0E4AA64E055326B8A54C9AE2AA64D7E5812A64F6C484AD27228533ED8A5675C08533D7164A5812A64E0558C7155B518ABFFD100A73D0DF3B57538108853F862AACA70B12AEA7021594E2AAEA7142BA9E98A1594E2AAEA6BF461415656C5055D5B0215D5B142AAB62A8A56D86062ACA7155756C55595B0315756C55595B2255541C55541C555037D07155407EFC5550362AAA0FDF8AAF0715540DF462ABC1C55783F4E2ABC37862ABC1F1C557F238AAE07C3155C1BC7155E0FBE2AB8118A575702AE0D8AB75AE2ADD4E155D5FA7E78A5BAE2AD823156EA7021BE5ED852EE5ED8D2BB90C34AEE431A2AEE43C31DD56F23E1852EE47C30AECD6FE38AB58A6DAA8C55AE58A5AE44E286B0DA86EB8AD355C556D7E8C556E2AEC55693E184056B155A4F861E6AB70AAD27155B8AAD27B0C556E2AB09C556934C556636AA64D4E455693B62AB302AD3D712C8354C0B4A742314375C0595B5914BA83C31452D23224AD3447B605A5B4F6C52BB8FB644A1785C8AAEE380A5704C0ABC260B55E13025704C5900BB8FCB16402E09E3F760B6402F0982D980AAB1FB606602BAC782D980AA13033015D63C0C80442C7D36C04B301149174DB22CC045247ED81988A2523F6C5922523F6C04AA2D22E9912551491FB604D22D23C169A45C716054F74DB1B697D7BDD52ED34AD034B4FAC6BBACCC78C36D6EBF69998EDC9BA281B93D32B9C88D80B2790EF6C8441DC9A88E67B9E03F9C3FF391AFE63B8FD0FF009756F268BA0D827D5AD35B9471BB78D070ADBA7FBA0115F8BFBCDFAAD48CD8E8BB3BC31793727F1F1FB9D6EBB59E34BD3B44727C9CCC496624B3312CCC4D492772493D49CDAB80A2C7AE295263DB0F34A9138495523BE2AB09C2AA6C6A7E58AA913D4E2952C52EC55D8ABB155A4E2AB715762AB4B7862AB7155A4D32252B315689A62AB315689A62C80584F8E05B584D714F25A4D361D707355B839AB44E2953AFDD815693E18A54C9ED8AAC6C55613812A64F8E2AB3A0AE29E4B30150B09C0953270254C9ED8AA993DB02AC27B63690A67C302A9934C095A70214D8D3A629533FEDE2A1489AE04AC638A5613815489EB8AB5C8E0A0AFF00FFD22C56CF437CF295D4FF006E042BA9C50AEA7FB3155704E160ACA702ABA9C50ACA7142BA9C2AACA702AB29FEDC514AEA70B12ACA7021581C55108DB0C0828856C50ACAD8AAB2B7D18AABAB60625595B0155656C0AAA0E2AA80FF00662AA80FFB78AAA86FA3155E0F8FD18AAA03E38AAA06C55786FA7155E0F87DD8AAF07155E0F8E2AB81F038AAF0DF462ABEB8AB6187CB155FC8E2AB81C095DCB156C1C55BE430AAEAFBE2AD8249C52D934ED8AB61BDCE36ADD4E2AEE47C30D2BB97B634ADF21E187756B97B615772F6C52D723E18AB5538ABB156B6F1C56DA269852EAE2B6B6A71575714358A5DF4E2B6B6B8A1A26B852D134C6D56938695AC2AB49C556E2AD138AACC556938AADE98AA99380AAD27B605598AAC26A714B581569EB5C0905D812EC0556D0605A750628A6A832169A6A95E9F8E29771C16AB82E450B82E0B4AF099155C137C0AA813155DC69DB025704AF6C0C805DC6BDB05B3A5E23F6C6D900BC2606602AAC7ED8096602B2C782D980AAA981980AEB1FB60B6548848F03301149174DB05B3011291FB6066022523F6C04A514917B602551291FB645348A48FA6D8DA51491FB6455191C449000A926800EF5C04A597E99A0235E45697FC9AF5C07FD1113719511BA3DD3D08814F604176FD95EF94CB26D63977FEAEFF00B9B618C5D1E7DDFAFBBEF7CF5FF3939F9976D752D9FE54F96248E3D134075B8F334B6C38A5CDFD0158BA9256106A6A4D58EFBAE6C7B2F48637967F51E5E43F6B81DA1AC193F770FA47DA7F507C80C7370EAD4C9C55498F6C592893920AA4C7F1C0AB09C2154CEDBE155263DB1485263DBC314ADC55D8AADAFD38AB55AE2AD62AD74C556935C0AD62AB49F0C04A5662AD134C55662AD138B2016629584D7FA605029A269F3C1CD5662775689C09E4B3AE0501613DB14AC2698AA9938AACC4AAC26B812B3DF14AC249C55613914A9938154C9C52A64D315532698A5613F7E46D54CF8E0559D715584E04AC3D2B8AA931C09532714ACC0AA67738AACEF5C090EC1BA6DFFFD32853E19E86F9EABA9C5895753FD9810AEA71556538A085756C2C5594E042B29C555D4E2856538AAB06E9855595B0215D4E162ACAD810AEADB62AAEAD8114AEAD8A1594E2AACAD8AABAB6062AAAD8295595B02AA83F48C555037DDDB15550DF778E2ABC1FA462AA81BE9C55783E18AAA06F0FBB155E1BE8C557F2F1C557838AAE0D8AAF07155C0F81C55772F1C55703E07155DCBC7155C0FBE3495DCBC702B61BC70D2AEE55F0C55BE5E076C425B0DEF8695BE58DAB75F0C76577238D056F97B61A5772F6C55DCBDB0AB5C8E2AEE47156AA7C714B5BE2B6EC536D12062AD72F6C34AEE58AB8D7156B15A6B1575462B6B49F0C2AD634AD134C92AD27156B155A4E2AB7155A5BC3155A7C4E2AB09AE2AB49A0C04AA9E0568F4C0AB314D3A98169D4C6D69AE3EF91B4D3B8FBE05A753014D354C16B4EE2723696C2E0250B82FB6455705FF3182D5784C095409FE6302AF0980AAEE382CA405C1705B301784C0CA9784C5900A823F1C16CC0550982DB005554F6C0CC05658F05B2A5658FDB05B20110917B60B6C011291FB6066022923E9B60668958FDB236A8848FDB02691291F4C5691291FB602528B48BDB02A6F69A6C93C325DC8F1D969D6E6973A95CB70810F872EACC7B2A82C7C32129D6DCCF73310B16761DEE87598CB5C45E596FA85B59A73D57CE77A9478633B5608B7F4CB74502B231E9C709C7FCFDFBA23F4FE29878DCF836039C8FE8FC5B1CF3A79F2DFF002EFCB57EFA72343AA4ADE8DA4339E570D7B327206E4EFCA558DBD594748D4A45F6A4739760D39CD304F2FC72F2EEEFE7D0389A9D4700E08EC4F3EFF8F99FB06DDEF80E79A5B89A6B8B895A7B89DDA49E77356777259998F7249A9CDE8D9D6A1C9C2AA6C7A9C5905127F1C5549BC30AA9135C2AA64F7ED8AA9B1FC30AA931EF8A54F14AD2DE18AB5527156B15762AB49C556D6B82D2D1DB128584D7025AC55696F0C524ADC50D13E18B30296138A39AC3BE0B4B44D361D702ACC1CD5A2714AC27EEC0A1613E1D714AC27155327EFC5565715584D7025657150B09AE29E6B49A6452A64D3155327BE04A993DF15584F738AA913DF02561EB912AB09AFCB155B5C0953AFE18AA993D4E29E4A44E04AC26B8154D8F6C55613BE290B58F6C01277594182D69FFD424539E86F9EABA9C50AEA7A7E1818AB2B62AAEA7FB715575385895653810ACA71556538A1581C50ACA7F1EF8AAB29F7C2AACAD8B121595B142214FBE042B038AABAB6042B2B74C50ACAD8AAB2B62AACAD818AB2B634AAAAD9155556F0FBB155407C3EEC55503787DD8AAA06FF6B155E1BE8C55786C5550362AB81F035C55786FA3155E1BC7155C1B155E1BC7155C1BDEB8AAEE43155C0FD38AAEE4314AE0DEF5C55BE5F7E1015706FA30156C357B64A92EA8FE6C55757DF05AB75AF7C765774C2ADF238ABB91C2AEE4715754E2AEA9C55ADFC714BBE9C56DAA8F1C55D5FA70AB55C52D72C55D5270ABAB4F7C08DDAAE29A6B156AB4C2AD1638DAB59255A4E0A56AB855A2698AAC26B86956D698156935C556934C0AB2B815AC0AD1E98A4358193B02BB015B750E05B753DF236AD918094B414F860B55C172255771FA305AAF095C04AAF099155409815502646D5771F6C0C806C27B62CC0540982D952E098DB2015447D0E46D980AA13FDAC5980AAB1E0B6C015963C8B2015963F6C59008958BA60B66022163E9B6066022522F6C16CD12B1F4DB229A5758F14A2523F6C16A8948FDB02A6369657177325BDAC0F713C9F6228C72269D7E81E3919480165946249A0AF777FA2685549D935ED557FE95D6D27FA244DFF17DC2FDB23F963FA5F18C273F21F6FCBF5A279218FF00A47BBA7C4FEA481E6D4BCCCCFA96B97E2C743D328AF32A71B7B70DB882D605A0691BB28DCF5634DF2D0238F688B27F164B8F294B2FAA66A23E43C80EF75E6B5F54834D1690C5A619C4973E54D2AE4868ED61881F5F5DD48D28FE92A93183B161F08E2BF118E3B26F7EF3FEF63FA5AB2E7E0882057F347FBE9779EEFD4F8DBCF5E69FF146B064B679BF4369C1ADF4649C932BA339792E663DE5B8726490F89A74519B8C38F8079FE36F83AEF7B0727B65CAA6C714A931C52A44F53F762AA24FE386D54D8F6C554C9ED855489EC30A429138A429935C55AC55D8AB44818AAD26B8DAB581569389295BD702B58AAC26B8AB58B2A5A4E290296138A39ADDCE0252B49EC302ADE980AB55C09595AFCB1501613DB14AC2698AAC27155327BE2AB09AE04ACAFDD8A8584D714F35A4D32295327155327B9C09584F7C554C9FBB15584D7E5813C94C9AE05584F6C0AB09C0AA677C52A64E295338142C270256134C554CE2AB3A0C09E4B301486B90F7C0BBBFFFD520539E86F9EAB29C5510A7162ACA702AB29C50AEA70AAB29C582B2B6055656C50ACAD8AAB2B62856538AAB2B62AAAADF7E2C5108D8B15756C55581C55595B020AB2B6285656C55595EB8AAA86C08560D8A1555B052AA06FA302AA86FA3155E0F8EDEF8AAF0DE3F7E2AA81BE9C55786FF006B155E1BE8C55786C55783E18AAEE43155FCB156C1FA3155E1B156C36155FC8FCF025B0D8A1B07BE1E695C0FDD8557576C0AD83F4E295DC8786156B96156EB8AB75F7C55D5F7C55D53E38ABAA7C715754F8E2AEA9F1C536D57DF1575478E2AD721855DCB14BB962875714DBABF4628DDA2714D355F13855D5C95AADE58AB55C55AAE2AB797862AB71568B62AB49AE2AB49C0AB09AE02AD62AEC0AD1DF025DC712AD803FAE42D34DD314D0760B5680FA72295C14E0255785FA72085C1705A5785C0ABC26055554DBA6449554E2322B4DF1C5980BC25312CE9785C16C8054095C16C805554A606602A04C16D802AAC782D92B2C7ED81900AEB19F0C16CC0442C7ED819808948BBD3012CC0442474ED9164110B1FB62C910B1FB60B544247ED815131C352A002598D1546E493D8604D26F716D61A2A893CC374D69211CA3D1E001EF5EBD3921F86107C6435F0539104CFE91F1E9FB5948471FD67E1D7F67C58B6A7E6ABDBD864B0D3E15D17497DA4B3B76264987FCBC4C68D27CB65FF00272F86011DCEE7F1C9C6C9A894850D87E39942D86956EB69FA5F5995ECB4646290F003D7BB917AC56CA7AD3F69CFC2BDF7A0C94A66F863B9FBBDED7180038A5B47ED3EEFD7D14B54D6ED96C13CC7AF5B241E5ED359EDFCB1E56858AADDDC0A13183F68A8D9A794EE7EC8DC800C319BE18FD4799EEFC740D73CA2438E43D23947BCFE3EA2F08FCC9F33EA16F15EE937B71EAF9ABCCA22B8F38CEA028B5B550AF6BA5C6A3640005791474A227ECB667E0C63623E91CBCFBCB832999C8CA5CCBC309EF998852271552638A42931FC714A931FC3155227A9C2154C9F1C2AA24F5FC3155327EF384325263DB155B8AB44818AAD26B8A5AC00AB44D31250B49AE04ADC55A2698B21B2CC58BAB8A405849C0CB92D270A296FB9C04A5693E1D307255B839AB55C0AA67FDBC521693DB14AC27A8C556138AA9935C556135F9604AC27EEC5405A4F618A79AC269812B09EF8154C9C095326B8AAC26B8AA993DBEFC090A64F6C16AB49A60553C55692702AC276F7C521409C052B49C52A64F738156138A56570150B09DFDBB6048584F6C095B8ABFFD68DA9CF427CF55D4FE1D70AAB2B62AAEAD8B12ACA7FB30215D5B1556538A90ACAD8B15656C50ACAD8A1594E2AAC0E142AAB62AACAD814ABAB6DD70B12AEAD810ACA7142B06FECC55555B020AB2B628560715550D8AAB2B7DD81055437FB78A1555B052AA86C055786FA715540DE1F762ABC37D18AAF0D8AAF0DF4E2ABC37FB58AAFE58AAE0DF4E2ABC36155C1B155C1B155C1B155C1BE8C55757DF0856F97B6295C1B156EA7155DC88F7F963B25C0F7C55BE58AB75C2AEE5E38AB7C862AEA8F1C55BAFBE2AD5462AEA8F1C536EA8C55DC8615DDAA8C56DD5C56DD518AB676C56DAA8C52D72F6C2AEA938405A6B1A55B51F3C695AAE34AD615689030AAD24E2AD6055A4F862AB70156B02BB02BB14B74C09A6F225374DF1F1C0BCDD4FF006F209A6F8F89FA3155C17E8C8155E17021771C052BC29C8AAA84C04AAA84C892AA817DB0269B0A4F6C5980A8129DB225952E09ED81980A8A98DB2015427B60B6602A2A606C015963F6C0C959531654AE917B6066022163F6C16CC044245ED91B6748958F02405658FA6D809648848FDB02512B1D29B6054E5B4E8AC214BAD72ED346B671CA24954B5CCC3FE2AB71F19AF8B715F7C889711A88BFBBE6CCC4445C8D0FB7E0122BBF38B40AF0796AD5B4A4238BEAB29125F383D78B81C6107C1057FCA3964705FD66FCBA7EDF8B44B535B40579F5FD8C33E276692462CEE794923124927A9627727325C564B1585968D6F0EA5E6146769D049A6680A4A4D720FD99262378A1F7FB4FFB3FCD9571199A8FC4F77ED6CA8E31C53F80EFF7F70FBD2D9EEFF4A7D6FCCDE68BA36DA169412290400254D098AC2C63FB219BB01B28ABB7BCC0E0F44399FC596832F12E733E91F8E11F8F3799EB5E6C94A49E7DD5EDA18D2C89D3FC81E5C1BDBACD1FC4B453F6A2B5AFA9231FEF242A0F534CBC78847D03DF23F8EFFB03879321C92B3F01DC1F375D5CCF77713DD5D4CF737575234D757321E4F248E4B33B1EE4935399A000842938554C9C295163F86295263F79C55458E2AA44EF8AA993E3D7F86155227BE14A993852A44D3155A49C16AD60E69761B55A5BC3012AB315762901693E18A16E2C86ED12315A59816D693E18534D76FE191B55A4D7E58F255B815693812B3AE2A02D27B6295876C55613E38AA993D715584E04AC26B8A85A4F6C52B7A60295327BE055326B812B09AE2AA64F6C554C9ED81216134C0AB09A605598AA993EFB604ACE58AD29B1FBCE04AC2698A54CEF8156138A54CEFF002C6D405A4F61F4E47CD3CD6134C052B3DF155BC8E2AFFFD78BA9FEDCF427CF9595B142BA9C2AACA7C7142BA9C50ACA7021595BFB30AAB06C0A42B2B7D38B0560D8A1595B155556C55581C50AAA71557538A0AB2B7F6E2C4AB2B7F662AACADFDB8A1541C55595B021595BFDAC50AAAD8AAA86FBBDB155656FEDC50AAAD810AA1B15540D915540D8AAF0DF4E2AA80FBE2AB8362ABC362ABC362ABB96155FCB155C1B155C1B155C1BE8C55706FA7155D518557723E38A5B0D815B0DBE155DCB14B7C8F89C55B0C70AB75C55C1B156F962AEE431577218ABB90C55BE4315772C2977207B62B4EE54ED8AB8918A86837F98C5496FE9C56DAAFBE2BBB55C212D54E2AD61DD5D51855AE58AB5538AAD240C55AE5E18556E057602AD6057604D2EC8B2762AE031250BC7B6413C9B03C705AB614FCB23695E17FDBC04AAF0B91285FC7E9C8A57853E1F460B55554C892AAC129F3C892ABC2E0B480BC260B6602F0B81980BC25716602A04C16CC0540981900AAB1E06602B08F1B6402AAC75A6064022163F6C16CC0442475ED8096C0112B1F4C8B25754C16C805748FDB05A5150C0F23A471C6D248E6891A02CC4F80037380964026B3D95A694036BFA8C7A51A5469EA3D7BD61FF001810FC1FF3D0AE44133FA45FDDF3652021F59AF2EBF2FD691DCF9CBEAD58FCB76034CA6C355B9E33DE9F75247A717FB05AFF009596474F7F59BF2E9FB5A25AAAFA0579F5FD8C3E5926B99A4B8B995EE2798F2967958BBB1F1666249CC902850714924D9E6AD0C124D2470C31B4B34AC122850166663B00146E49F0C04D2817B06492B597957FBF58753F332FD8B23492D6C1BF9A7EAB2CA3B27D953F6AA7E1CA80393CA3F69F77706C94A387CE5F60F7F79F2E9D58F410C9AACDA8EB3ADEA2F069F6BFE91AF6B93D5D806345551D5E590FC31A0EA7C14122E278408C46FD07E3A38E01993291DBA9FC75603AA6AC7CEBA88573FE1FF26796A0927F4EBCC5959823D49A43B7A97131A0FF0029CAA8A28CC8843C21DF23F69FD41C5CD94E43DD11C87E3A9EAF0DF36F9964F336A9F59480D8E976310B4D0B4A06A2D6D1092AA4F77624BC8DFB4C49CCBC70E115D7AB0018A13FED65895227B0C2AA4C71641498FDC3155163DF155227155327BF6C55449C290A64FDD86D2A44F7C2AA677C052D6D8ECAD16F0C16AB49AE2AD62AD138AAC26B8A80EC534B49C59725A4E28E6B3AE04B550307355A77DCE36AD1C8AAC27EEC55613DF14AD2C7BF4C52B09C556134C55693E38AA993812B49C569613E18A56134C052B09C0AA64E04AC26BF2C554C9ED8AF2584E0480A64D3DF02AC38156935C09584FDF8A1489DABF7629584D314A993812A64D702AD27B62958715A584D302ACC892C82C26B8AA9935C55AC55FFD08983DF3D09F3E56071415653F76155653FD98AAB86FF006B1452B038A1595B142B29C0AAC0FF006E1410ACA70315556C55583628550D8AAAAB6142214ED8AAB29C08A550DFED62821595B142AAB74C50AC1B155456C0AACAD8B1560DFED62AA81B15550D8AAA86C514AAAD810AA1B1A55E1B22ABC362AA81B155C1B155E1B0AAF0D8AAEE58AAFE58AB61BE8C557F2C2AD823C698AAEE47E78AAEA8C52B81F7C55BAEDEF8AB75C296C1F6C55BE43155D5F7FC715757DF156F91C55DC8E2ADF2C55DCBDB156EA3156B961577238AB458E290BF143A98A6D6D7DC62BBB55F9E14BB91C514EA9C904ADC55DC862AD72F0C55AA9C55AC55D518ABB0156B02BB02B630320DE04AE03C7AE0250376FAE4494AE03FDBC8A5785FA7DF1B5540B9592ABC2E0B55E1722AA8171B55658FA7EAC812AAA12991B501785C16C805E1305B301785F6AE2CC05454F6C16C805509916C015027B62C805558FDB1B6602A84F6C0C805658EB8B20110B1E44966022162F6C0CC0442A0C8DB35654F1C09A4E2D746BEB884DD0844164BF6EFEE1960807FCF590AA9FA0E40CC035D5B040917D1426D47CB1A60224BC975EB91FF001EDA7831415F06B9956A7FD821F9E484272E95EFFD4C259B1C7AF11F2E5F3492EFCE5ABCA8F6FA62C5A05A38A3C5600ACAEBE125C3132B7CB901ED96C70446E773E7FAB9344B5533B0F48F2FD7CD8B05A92C776635663BD4F89CB9A02BAA7B624A536D334ABCD5273059C61BD35325C4EEC1228631D649646D914789CAE73111659420667646DD6B769A3C7258F96A432DD48A63BFF3315292383B34766A77890F42E7E36FF246D8C7199EF3E5DDFAFF0014B2CC21B63E7D65FABB879F32C7F4DD385E1B89A7B88F4FD334F8FD7D5B559ABE95B435A726A6ECCC76451BB36C32D9CEBCC9E43BDC7842FAD01CCF7303F32798AE7CD97961A0797ECE68344B69FD3D13481BCD713C945371714D9A593EE45F846D5272316218C1948EFD4FE81E4D39B371FA63B44721FA4F9BCE7CF3AE5B5B40BE4BD16E52E6C6CA61379875388D52FEFD011446EF05BD4AC7FCCDC9FB8A5D8A3678CF3E9E43F6B500F2F63DB2F4A931C2AA44E1485263FD98A5498F6FBF155127F0C55498D7155263E1D06295263852A44E14A993F7605585BC31B55B5C55AC55A2698A56935C516D62AD134C5980B3F1C0825AA8C29A5BD7012AB49F0FBF07BD56E03BAB5518156138A56934DF14D2C26BDF15584E2AB49A7CF15595EF8AAC26B812B09AFCB155A4E29E6B7A6029532702AC270254C9ED8AAC27B62AA64D31480B09C0AA7EF91295A4D70216138A54C9FBB1553638A54C9C090B09AE055A4D3155327E9C556F4180B2E4A67C7012901693DB02A993DB15584D3155B53E38ABFFD187A9CF427CF9594FDD8AAAA9C50ACAD855595B155756F7C50ACA7021555B142BAB62AAA0E1415656C58AA86C08560D8AAA2B62A8856C28560D8AAA86FECC08215437DF8A085556C58AB2B62AAA1B15550DFDB815543628550D8A154362AAA1B155456FA0E28A550D8169503628540D80AAF0D8AAF0D815786C2ABC362AB837D38AAE0DEF4C34ABC362AD86F7A62ABF962ADF2C557F2C52D86C55B069F2C295DCABD0E2ADF2A77AE2ADD7156EA3DF1577218AB751E38AB7CBDF15757DF156F91C55D538AB75C2AEE4714B55F1AFD18AB7518A1DC86297123156B961A571627C30EEB4D570EE96AA3C71575462AD72F0C55AA9F1C2AD605762AD83DB224AAEC0AD8C0521BC0C97014C8AF25E062ADD3C7209540BEDF464495550B915540B80955E1722AAAA9912555826450A81703201784C8B2A550BED8B3A5E17E8C896402A84C6D980A81303301515316402B2A7FB5819855098B2015963F6C04B30110B1F4C892CC05758C6DE27A7BE0B6602770E87A93C5F5892DBEA96B4AFD72ED96DA2A7B3CC501FA32B3907C7C9B463973E43CF6434B77E59D3EA2EF577D5261FF1EDA5C6596BE06E26E09F4AAB6484672E42BDFF00A984B2638F337EEFD652D97CE6D09E3A2E8D69A6D3ECDDDC7FA6DC7CC19408D4FCA3C98C03F8893F606A96ACFF000803ED3FABEC6337DA86A3AACDF58D4EFA7BF9BB493BB3D078283B01EC32E8C4445014D129CA66E4495054C285554C5202BAA7B60B4B20B1D1E3FAA8D5756B93A5E8A0909725794D72C3AC76B1120C8DE27ECAFED1ED95CA7BD4773F77BDB238F6E291A1F7FB92DD5B5F92FE11A6585BFE8AD0A260D1E9C8DC9E571D25B99280CAFF00F0ABFB2064F1E2E136773DFF00A9A72E6331C2368F77EBEF41E9DA6B5F34F2493C761A7D8C7EBEA9AA4F510DB420D0BBD37249D9546EC761939CF87CC9E43BDAE10E2F20399EE79FF9BFCDABACAC7A3E8F1C961E56D3E4325B5BC94135DCC050DD5D5362E47D95E88BB0DEA4DF870F0FAA5BC8FD9E43F1BB466CDC5E98ED11F6F99FC6CC6B5ED53FC11A63D85B9E1E72D72DA974E3ED695613AFD8FF0026E2E10EFDD233FCCDB5B11E21B3F48FB4FEA0D203C449000A6D4D80CC966A44F5C554C9C925489C52A2C7155163FDB8AA9B1C55458E290A44E2952270AA9135C52A64D7155B8AD5BAA06295A4E2B6B714344D31480B49AE2CB9355A628E6B09AE04BBA60B5584D7DB1E4AD602AB6BF86055B5FEDC534B49A629532698AADC556138AAC27C702AC2714AD26BF2C542C26B8A79B44D30254C9EF815613F7604A993E18AAC27B77C554C9A604AC269F3C1CD567BE24AAC26B8156934C0AA75FBB14AC635E98A792913F760B5016138A5613815613DF14ACF738095584D7072480B09ED8156134C521613DF15584D7156B02BFFD28529CF427CF9594E2AACA71555538A1594E155656C55594E282ACA7142B038A1555B02AB06C2821595B031550DE18AAAAB7FB58A15D5B6C555436142AAB7638AAB29C0B4AA1B0B1A5556C5042AAB6042A86FF6F15540D8AAAAB5698114AA1B15550D8A154362AA81B15540C71452A86FF6B0229786C555036055FCB155C1B155FCBE8C34ABB962AB837BD3155FCB156C37BD3155C1BE9C55706C52BB97BE2ABB96DD7156F97B614BAA3DF155DCBC3156C37BE2ADF2AE2ADF2C55DCB156F90C55D51855BA8C55AA8F1C52DF218A1D5C55BAF8F4C55C1BC3A6296AA315A6EB868AB553845ABABEF855AE4314B5C862AEE47156C1AE2ADE2AD646D5B1D462ABF02577CB236C97819155E07D270257D3FB7012ABC2E564DAAA85C16AA817224AAA85C16AAAA9912555C2D3B640955E17B604D2A04C0CC0540BED82D952A04C16CC055094ED81980A8109C5952B04C16CC0550981900AAA9520773D0624B3013AB7D0B559D3D64D3E6583A9B9947A5153C7D4938AFE3959C91EF6D18C9E8D3C1A4D9D7F48F986C612BF6A0B52D7B2EDDA908280FCDF11C47944FDCA4C23CE43E1BFDC847F31796AD6A2D34DBED5DC7492EA44B4889FF8C717A8E47FB3192F0A679903ED6275101C813EFDBEEFD68293CEBAC8AAE9B1DA68687A3594204B4FF8CF2FA92FDCC325F978F5B3EFFD4C4EAE7D287BBF5F36377373757D299EFAE66BD9DB769EE1DA473FEC9893968023B0D9A244CB726D62A6285554C5900AC17164ACABDB05A4044C30493491C30C6D34B2B048A28D4B3331E815454927012902CD0647345A6796ABFA5638F56D7547EEF4256AC16C7C6F2443F130FF007D21FF00588E9958E2C9F4EC3BFBFDDFADB646387EADE5DDD07BFF0057CD89EA3A95FEAF746F351B83733F108868152341D238D1405451D954532F840405071724E590DC8DAB69DA71BDFAC4D35C4761A75847EB6ABAACFB436D1569C9A9B9663B2A8DD8EC3194B87CC9E43BD108717901CCF73CFF00CDDE6E5D6963D1F478A4B0F2BD8C9EA5BDB494F5AEE6029F5ABA2362E47D95E88361BD49BF0E1E1F54B791FB3C87E3768CD9F8BD31DA23EDF33F8D9236BBB6F276996FE64D4228EE358BE52DE50D1A601958A9A1BFB843FEEA8D87EED4FF0078FF00E4A9CB2B8CF08E5D4FE8FC726901E217777737D73717B7970F777777234D75752B1692491CF267663D49273240AD83341138554C9C212A44D30A69458FE18AA9138AA931FEDC55459B164029138AA91385548B6295227155951F3C534B49C56DAC546EEAE04D2D2714ADC28B5A4F8629A6B012AD134E9D702ACDCF5C16AEC55693F7605587A629A5BCA9DB14AC27EFC55613DCE2AB49C55613812B09F1C556938A696138AF35A4D32369584E2AA64D70256135C55613DB14A9934C0AB09A75EB839AACF9E24A56135F96042D2702A9F5C5563376ED8B25226B80AAD2698A54F02AC2715595AFCB0242D26B8A79AC269F4E452B09A62BCD4C9C556135C556934F9E290B7EFC536FFFD383039E84F9FAB2B76EDDB142AA9C55541C555836142B2B7DF8AAAAB62AACADFEDE282ACA7142AAB628560D815595BFDAC5042A86C58AAAB6155756E9810AA1B15550D8A1555A98555437E1815555BFDAC514AA1B162AAAD8A154362AA8ADEF8AAAAB6042A2B7FB58AAA86C50A81B15550DEF8AAA06C555036042A06F7C514BC37D18AAF0D815786FA70AAEE5F462ABB97D38AAE0DEF4C55706FA7155C1B14AE0D8A1706DF0A5706C55C1B7D8E296C37F6E2ABF962AE07C7156F90C55BE5EF8AB61BE9C55BE4715772C55BE430ABAA314BB90C50EA8C55DCABDB1485C0E2A56D70ADB753E38695AAFBE1A575462AD72C52D835C55BC55C0D3155FD72255D8AAE5C095F812BC0FBF204AAA014C095403C3229540B8095550B902AAA1722AAA1722AAEA9912AAA140C8DAD2A05AE45900A81305B301502E06402A84F1C0C80578A279484851A563D150163F4018096C03B9398FCBFABB2891B4E9A18CFF00BB6E00813E7CA52A322663BDB0632B5ACACADEA2F75ED32D48D9A2498DCB8FF636CB27E271B27903F8F7AFA4732141B50F2ADBF5BED435161B15B6B64854FF00B399C9A7FB0C3C333D005F1603BCA1DBCD1A745B58796E373DA5BFB99673FF000117A2B92184F597C91F981D23F343BF9C7CC142B69710E9487B585BC501FF00830A5FEF6C3E043AEFEF47E667D0D7B920BABABBBF7325F5DCF7B29EB24F234ADF7B93960023CB66B3232E66D4827DD85202B04F01F4E46D2A817E9C0C95157155654F6C5980AA1714AB2A602520275A6E8F3DFC72DD178AC74DB523EBBAADC9290455E8B5009673D914163E195CA75B7327A36431996FC80EABEEBCC56F611C965E56496D964531DD79826016F2753B158C027D08CF829E447DA6ED86388CB79FCBA7ED633CE23B63DBCFA9FD5F7B1155F6CBDC609B5869F1CF15D5FDEDD2699A2E9C036A7AB4A0948837D94451BC92BF4441B93E02A44252AD86E4F21F8E8CE31BB24D01CCFE3ABCE7CD9E6E7D7BD2D2F4D81F4CF2CD8485EC74D2C0C93494A1B9BA61B3CAC3FD8A0F857C4E4E1C3C1B9DE47F143C9C6CD9B8F61B4474FD27CD258134ED134C5F33F98A2F5EC59DA3D0F45E4524D4EE13AAD46EB0467FBD71D7EC2FC476992647863F1F2FDAD203C835BD6B51F306A775ABEAB3FAF7B764732A02A2228E291468364445015546C065F18888A0D89313F76495498F7C290A44FDDDB0A5498E155166FBB155227155166FEDC5900A24E2AA64E14A9138AA93362AA44E2AB715762C82D27B0C52B715B68B628A5BD7025AA81839AADA93F2C792B5815A2702AC2714F3689A75C52B09C556138AD2DAE2AB2A7155A4E04AC27155A4F7C53C96135C556934C09584E0553270256135C55613D8605532698A40584D3073559EE712556135C095A4D3021613F7629585BC3E9C542913D8604AC269812A64E2AB09AE2AB31B4D2D27B0C09E6B09A6452B09EF8AAC26B8AAC27B62AB09A604ACC54ADE43157FFFD480AB74CF4278156538B15653DB142A838AAA86C55554E285656C2AAAAD8AAB86C514AA1B142A86C085656C5554361410AA1B0315756DBF862AAAADE18A1555B15550DE18A1555B0AAA06C0AAAADEF85042A86C56950360634AA1BB628550D8AAA06C5550376C08A540DEF8AAA06C50AA1B15540F8AAA06C55787FA71452A06C569786C085E1BE9C55706FA3155DCB155C1BE8C52B8362ADF2DB0AAFE58AB61BDF14B75F6C55B0D8AAE07DE98AB60E2ABB91C55DCBDB156F90C55BE5EF855BE5EF8ABABEF8AB75C55D5C55B07157723E38A87723E34FA70B26AA315DDD5C2295DCB0ABB962AD835C55BC21578351815BC16AB97A6056F0257818142A814C892954029F3C894AA01914AAAAE44955655C812AAAAB9155609F40F1C04AA22289A42046A6424D00515FD5903254E61D075998728F49BB29FCE62645FF826007E3951C91EF6631CBB951B469A0A1BCBBD3EC0743F58BD8148FF0062AECDF863C57C813F065C07A90A4534482BF58F32DA123AA5AC37139FA0FA68A7EFC3523D13511D56B6A3E558B6136A97C69FEEB821B75FBDE490FF00C2E3C133DCBC701DE54CF98B478BFDE6F2EB4C47ED5E5E3B7FC2C29163E11EFF00B17C60390533E6DBD5FF007934DD2EC69F6592D56661FECAE0CA708C03A927E2BE3CBA5043CDE6AF324EA51B5ABA8A33FEEAB77F417EE8820C90C301D1072CCF54925925B862F3C8F3BB1AB3C8C5C9F996AE4B60C79F3705F6A53164BC2D3032540B8A405E1716402F55C0C80A560BF7E0B4AF0BFED6064AAAB8AAB2AE2CC05555FF006B012944C16F2DC4B1C1044F3CF2B058A18D4B3313D9545493F2C0480C802760C825B5D27CBF53AE3FE90D4D7ECF97AD64A046F0BB9D6A13DD12ADE2572B1C53FA761DFF00A8333C38FEADCF77EB3FA39B1BD575AD4359788DE48A96D6C0AD8E9D02FA76F029ED146361EE4D49EE4E5D0C621CBF6B464CB2C9CFE5D025AAB936B4DEDACED62B39B5AD6AE8E9BA0DA3F09EF000D2CF2D2A2DED50D3D49587D0A3E2620640C8DF0C773F8DCB38C45714B68FE360F2FF0034F9B2EBCC92C104700D3342D38B0D23458D8B2C7CB669656DBD499C7DA73F2145DB32716110DF993CCFE3A38D9731C9B720390FC754BAD2D74EB0D3DFCCBE632F1E850398ECECA36E33EA772BBFD5E03D946DEA494A20FF002881932493C31E7F73500F27F31798B51F32EA526A7A894562AB0D9D9C238416B6E9B470409FB2883A7727735249CBE10101419B1F63FDB92552271552270A548B7F6614A8B1C2AA4C7155166C52A4C70A5489AE2AA4C714A931C55498E2AB09A60654B4938A56E15B6AA0628E6B4927025D82D5696EC31A55B82D5C4D31559CB025AAE2A02C27AE29584E2AB49ED8AAC27155B8AAD2702565715A5B5A7BE29584D702D2D270734AC2715584F73812A64D715584F618AAC27026961381567CF12AB09AFCB02AD2702ACAD7155327B62C82993DBB605584D3025613DF15584E29595AE2A02D27B6452B3A60B4D2C26B8AA9935C556938AACE98A54C9AE2AB49ED8156E04BFFD5E78A7EECF427825656C5042B29C58AAAB6285507EFC55541C55555B142B2B7DF855555B155656C50AAADFEDE042A86C50AAAD8AAB06F7FA71415656FED185895556C0AAC1B142A2B62AAAADEF8AAA86C50AAADFD98AAA06F7C5550361452A86C0821503628550D8A150362ABC37BE29540D8114AA1F142A06C50BC362AA81BDF15540F8AAF0D8A1786C0ABC362B4BC36286F91F1C52B831C55706DB0AAEE5EF5C55772C52D822BD4E2ADF2F7C55772C55BE5ED8ABB90C55772F7C55BE58AB7CB0AB7CB1577218AB75C55DCB14B75AE286F962AD57DB0A5DCB0EEAEE5855D538A57038AAE06870AAFC0AD8EB8AAEC8955E3A0C095E37F9604AA0A0DCEC3B6449545C16D7170DC6DEDE5B873FB312339FB941C81901CD9004A731F96F5D650EDA5CF6E87FDD970040BF4994AD32B3963DECFC39772EFD0FE8D45DEAFA559B0EA8F769230FF006307AA7071DF207E4BC1DE47CDDC3CBD0D3D7F317AFF00E4DA59CAFF008CA62182A67A7DA8A88E67E41A3A8F95E2DD2DF55BE20FEDBC16AA7EE1391F7E0E09F904DC7CD67F88B4C8EBF55F2CC0C3B3DDDD4F29FBA3310C1E113CE4BC43BBED5BFE2ED45686D6C74CB223F6A2B2899BFE0A6129FC70F811EB7F35F10F403E4A5279B3CCD282A75CBB890FFBAE07F4169E0044131F0603A23C4972B4A26BABABA3CAEAEA6B96FE69A4673F7B138761C98924A92A8EC00F96055503FDAC0AA807DFDF02AA85C49554E391B50DD30325E062CC05E057164A81477C55781E18B25455191B6602B05FA30242F0BF40C592A05C55582E2CC0550B4F6C52C863D116D618EF75FBA1A2D948394113AF3BBB81FF0014DBD43107F998AAFBE55E259A88B3F67CDB380445CCD0FB7E010773E6878A292CBCBD6C744B390709AE4373BEB853D44B3803883FC91851E35C9470EF72DCFD9F2FD6C259CD54361F69F8FEA630ABED4F6CB9A1542FFB78AA6A534DD1F4E8F5DF323C9169F2D7F46E9B110B75A8BAEC561AD78460ECF29141D17936D90B323C31E7D4F41F8EE6742238A5CBA0EA7F679BC97CC7E66D47CCF791DCDE88EDED6D54C3A5E956E0ADB5A435AFA712927AF5663F131DC9CCBC78863143E27BDC5CB94E4367E03B942DAD74ED3F4E3E64F32BC90E88AED1D8D94442DCEA73A7586DEB5E2ABFEEC948A20DB7620619124F0C79FDDF8EE6BA795F993CC9A8799F50FAF5FF00A70C70A0834DD3A01C6DED2DD7ECC102F651D493BB1AB3124E5D08080A0CE98E31C9AA8938AA9138A5499B0A5498E155263855459B109512714A9938AA931A62952270AA9138B20A44FDF8AACC56DAE58AD5AD3BE04B58DAB5CBB0C14AD1DFAE36AD6055A4FE18156D714D2D2714AD24E2AB6BE38AAC27C7155A4E2AB49C52B09C0AB2B8A80D13D714ACF9E0485A4F8605584D3155327C70256135C556135C554C9F0FBF02696134F9E2AB3DCE0252B49AE042D2702ACC52B09AEC3E9C5429135DB0256134C52B2B8156138AAC26B8A42D269B0EB839A561F1C8929584D715584F6C556134C5561F1C556135F9629E4B09ED8142DE9812D571453FFFD6E6EA73D09E09594E285607EFC5485507162AC0F63D7142A03F7E2AAA0E2AAA1B0A1541C5554362AACADF7628560D8A15037FB7810AA1B155756D861410AAAD818AB06C5550362AAA1B142A06C55555BFDAC50A81B15540DEF8AAA06C5695437D38A295036282150362C55036295E1B142A06C55503E05A540D8A1503FF0099C50BC362AA81F155E1B155E1B15540F8114B8362AB837BE2AB83615A6C37B7DD8A5BE43155E1BDF156C37DD8AB7C862ADF2F7C55706FA7156F962AEE5855BE58AAEE5EF8ABB91F1C55BE58AB831C55782715712698AB55F7A614BABEF842BAA70AAF06B8AAE068714AFC2A88B7867BA611DB4125CC87A2448CE76F6504E449039A817C9394F2D6B740F3589B1422A1EF248ED853FE7B3265672C7BEDB3C29777CF671D2EC2115BCF31E9F11EBC2DFD5BB6FBE24E15FF0065838C9E513F72F001CC8FBDC64F2B435ADCEA9A81036F4A186D97E557794FFC2E03C67B827D1E67EC6BF4CE8F10A5AF96D243D9EF6EA694FDD17A231E097597D8BC511C87DAD7F8A350435B4B4D36C3B13059424FFC14A246FC707851EA49F8AF887A50F821E6F327986E5784DAD5E94ED1ACCD1AFF00C0A151F86118A03A041C93EF29548CD292D2B348CC6ACCE4B127DC9C904172F4E94F96449405D81485C3A64592E02A71405E053012A570190255781E1914AA01815540FA3155655F6FA3224AAA529915A6F16402F0316602A05F1C52BC0C5900A806025952A2AE46D900AA169EE714AA05FBF164A81715A550B4DCFE38B3019043A14C90477BAB5C45A269F20E515CDDD43CA3FE288147A927CC0E3E2C32B3937A8EE5B38285CB60A6FE63B3D37E0F2DD9149C6C75DBF5592E6BE30C5F1470FCFE26FF002861F08CBEB3F01CBF6B139C47E81F13CFF63169A69EEA692E6EA692E6E663CA59E562EEE7C599AA4E5C056C1C726CD972AFDF8A5500C5531D4EEF4CF26C4B36B70AEA1AF4A81EC3CAC588118615596FCAD0A2F7110F8DBBF15EB088965DA3B0EFFD5FAD948C716F2DCF77EBFD4F1BD6359D4B5FD426D5357BA6BABB9A80B502AA228A2471A0A2A228D955450666C31C602A2E24E6666CF3442C3A6683A743E60F332B3DB5C82742F2FA370B8D4994D3912378ADD4ECD2756FB2953B804991E18FC4F77ED634F27F30798753F32EA0DA8EA92AB38410DA5AC4BE9C16D027D88208C6C88BD80EBD4D4EF97C2020283363E5BFB324AA4C715512DF76295366C295163FD98AA931C36AA2C714A933614A913E1855489C095263F762AA44F7FBB155227EFC2C80532714ADDCE0577CF05AAD2DE03E9C6956EE7AE24ABB02B44E04ACAE2AEC5696161DB14ACAD4E2AD72C55613F7E2AB49C556934C09584E2AB4E29A584F874C55A26982D2B09AE05584F6C09584D315584F738AA9935F9605584F61F4E369584E0B55A4F8E05584D715584D30256F5EF8A1631EC3A77C52A44F8604AC269812B315584E2AB09AEC3155A4D307364B3012B4B09AE05E4B09ED8A42D2698AA9E2AB09AED8A56934C0AB3DCE295A4D702B58ABFFD7E660E7A13C12A29C55595BFB0E2856071410AA0E2C55837DF8AAA03F7E2AAA1B15550D8A15437FB7855515BC7155556C5558362C69515BEEC50AEADB605560D85550360634ACADEF8A150377C55503628540D8AAA86C555036142A06DF02AA06C555037BE28540D854AA06C514A81BFCC6062A81B15540D8AAF0D8AAF0D8114A81F15A5E1B142A06C55786C55787C557F2C557F2EF5C55703FEDE2AB837CC62AD86DBDF155DCB7C55B0D8AAEE5EF8AB7C8E2ADF2F1C55BE5EF8AB7CBDF0AB7CB156F962ADF2C55DCBC7156F97CF156F90F138ABABF4614B75FA715706DC0EE7A0C92A796BA06B5748258F4E9920FF96A9C0822DF71FBC94AAFE3959C911D598C723D15C6936506F7FE60B1829B986DB9DDC9EE3F74BC2BFECF1E327903F727807521BF5BCB16FF00660D475571B7291E3B38CF81E2A267FC463EB3DC3ED5F4799FB1DFE2048BFDE1D0F4DB33FB323C4D75203E20DC338FF85C7C3BE6495E3EE03EF519FCCBAFDC27A6FAB5C470F516F037A118FF00611041846280E8A7248F549598BB1673CD8EE59B73F8E4D835CB155C0839157604AA0C0A1774C092BF22C978E98A02E1DB2255777C092BA9BD323685403C302AFA76191252BD476C0AACA3FDAC0AAAA3012AAA078E41406F0B25E177C0CC0550314AF0B8A5500EC305B3015157C722C82B818A5781F462C95A389E4748E343248E6888A0B3313D801B9C6D2027EDA22E9EAB27982FA2D1148E42D1C7AD7AE3FC9B64355FF9E854657C7C5F48BFB9B38047EA35F7A19FCC96B61F079774D5B69474D62FC2DC5D57C634A7A517D01987F361F089FA8FC072627381F48F89E7FB18D5C5C5CDECF25D5E5C4B777329ACB73339776F9B354E5C000283412646C95A17A7EAC5695557FDBC5923EC6C6EF51B84B4B2B77B9B892A5625F01BB331340AA06E493403AE094844594C4191A082D5BCDFA6F95F9DAF97278B57F318AACBE60501ED2C9BB8B30C292C83FDFA4715FD807ED631C4726F2DA3DDD4FBFF0052259863DA3BCBBFA0F77EBF93C7669A59E596E2E2579E79DCC93CF23167776352CCC6A4927A93998000E19DD39BC7D33C9B6F15EF982D9750D7AE1165D23CA4E480AAC2A971A8528510F558B667EFC57AC6CCCD47977FEA480F1FD6759D4B5ED46E355D5AEDAF2FAE69EA4CC000AAA28888AB454451B2AA8000E997C622228726612866FECC92A913F7E2AA2CD8A5498FDF8A5459B0AA931C52A4CD855498FF0066295266C2AA4C698A5489EBF89C55489F1FBB1480A44F8E2952270256EC3BE3655696F0C69569DFAE36AEC0AD5714AD27E8C08B689C52B6A314AD27C3155324F8E2AD138AAD27155B8156138A5693F7E2AB49FA714AD3B9C55693914AC27C715584E04AC27C315584D315584D715584FDD8134A64F8636AB7A6F8094AD26B9142C2698AADAE2953635E98A54C9EC3A605584D314AC3BF5C0AB09AFCB15584F618AAD269F3C1CD2B3224A696135C52B09ED8A85A4D31559D715584D761F4E29584D302ACF73894AD26B815AC55ADF157FFD0E5A1AB9E84F0655436285656C55555BFB0E285607EFC5042A86C50AA0E28550715540DF7E2AAAAD8A15037DD8555437FB78AAAAB628550D8AD2BAB6D8B1550D8155437FB58AAAAB6282150362C55437F662AA81B142A06C5550362AA81B15540D8A150362AA81BE8C2ABC3605540D8B1A540D8514BC3E042A06C55786C5550362AB837FB78AAA07C08A5E1BDF155E1F142A06C55706C55786F7C55786F7C55B0DB62ABB90C55706F7C55BE58AB7CB156F97BE2ABB97CB156F97B615772F6C55BE5EF4C55772F7C55DCBDF156F97F958AA3ACB4ED475127EA16535D05FB4F1A128BFEB3FD91F49C0640732CE31279263FA1A0B635D4F5AB3B323ED5BDB937937B8A435407E6E3071DF21FA13C23A96FEB3E5DB5FEE74FBAD5641FEECBC944111FF9E5055A87DE4C6A47AD2DC474B5DFE26D4621C74F4B5D1D7B1B281237FF0091ADCA4FF86C3E147AEFEF4F19E9B24F71777378FEA5DDC4B7721FF764CED237DEC4E4C003930249E6A351F2C2ADD478E2ABAA7C715754F8E2AEA9C55B06B80AB7914AA0DF0150B81E98AAA60B495F914AE076C8DA85C3C702AA0C8955E06455781DF014AA01DF02AA28FBF02AB01815597C722569762C951462C8054030325455C095E07618DB2A5555FBF22CC056000153D314A6D63A36A5A8219ADED5BEAA9FDE5F4A562B741FE54D21541F7E44CC0671812AEC3CB9A77FBD9A849AD5C2FF00C79E99F0420F835D4ABBFF00B043F3C471CB90AF7FEA5E284799BF77EB42CDE6BD4151A0D1E187CBF6EE38B0B2044EEBE0F72E5A53F4103DB2430C799DFDFF00A989CD2E9B7BBF5B1BA1662C4966635763B927C49CB69A9500FBB014AA81812AA06294DA1B08A3B23ABEAF7B1E8DA223153A8CC0B199C758EDA21F14CFECBB0FDA20644CB7E18EE7F1CFB9988ED72D87E3977B02F31F9E64BFB69B45F2FC1268DE5F936BA0CC0DE5F53A35DC8BFB35DC44BF00EFC8EF96E3C1478A5B9FB07BBF5B4E4CD63863B0FB4FBFF5306B6B7B8BCB882D2D2092EAEAE1C476F6D12977763D15546E4E649A02CB4265A9EB7A7791CB4166F06B3E744A869D78CD63A4B7B1DD67B85FA5233FCCDD2B11393CA3F69FD8A03C6AEAEAE2F2E27BBBBB892EAEEEA4696EAEA662F249231AB3331A9249EE73240AD833A42337F66155127EFC5548B7FB78A5498E295266C52A24FDF855498E055266C29522DFEDE155227F1C3695227AFE3855489F1E9DB229014C9FEC18B25126BD3AF7C554C9F0FBF155B8DABAB8156F2C55A27E8C0AD57DBE9C52B49C534B4B62AB6BDFAFB62AD138AACAE2AB49AE2AB0B6055A4F738A56935F962B4B49A6C314ADC04A405A4E05584E05584E29584E2AB09F0C0AB09C53CD6135F6C16AB09F0C556934C0AB2BE38AAD2D815656B5C52B09EDDB14A9135C0556934C52B09EF8156135C556135DB155A4D3E7812B2BE380948584D70256938AAC2698AAC26B8AAC27B629584D302ADEBBE29584D76C0AD62AB49FEC38AADC55FFD1E4E0E7A0BC22B2B61556538B15556C55555BFDBC50AC1B1522D501C58AAAB628540D8AAAAB62AA81B142A86C2AA81BFDAC0AAA1B0AABAB6C3141550DFED6062AA1BFDAC5554362AA81BFDBC54AA86F138B1215031F1DB142A06C5550362AA81B142A06C5550362AA81BE8C285E1BDF02AF0DEF8AAA06C55786C505786F7C2C695036042A06C55786C55786C55786F7C55707FA302D2A06FA3142E0F8AAA07C50B83E2ABC362AB837BE2ABB97D38AB61B155DCBDF156F962ADF2C55BE5855BE5EF8AB7CA9DF154C6CB4CD475005ED2D1E4857EDDC9A242BFEB48E428FA4E44C80E6C8449E48DFA8E9369BEA1AB8B871D6D74D4F58FC8CCFC631F472C1C44F21F34D01CCAE1ABD95B53F4668B6F1B0FB3757A4DDCB5F10AC1621FF00018F093CCFE85E20390415E6AFA96A202DE5ECD3C6BF6202D48D7FD58D68A3E81928C40E4832279A001006C324AD861F2C2ABABEF8EEADD4E156F962ADF218ABAA3156EBE07156C1C557644AAF06B812BC541C0AA981257E452792A62A1703B64121775C542A819028541E1815781DB014AA81FD99155551DFBF6C0AAEA3EEC892ABF025781F7E2C805403166AA0604AF1EDDB164135B0D2352D4416B2B296E234DE49C0A44BEED235117E939094847996718928D6B3D1EC2A754D76169075B2D357EB92D7C0C80A42BFF06700323C87CF665E91CCFC941BCC76569B68BA24313AFD9BFD448BC9EBE2B190B0AFFC01F9E4BC227EA3F2D91E2D720925FEA9A9EAD209352BF9EF597EC095C9541E089F6547B019646023C8535CA465CCA100C921540C52A8076FBF01295455C0A8DB4B4B9BD9E3B5B3824BABA98D2282252CEC7D80C8C8888B2C8024EDCDDA9EB5A079579473187CCBE604D86990BF2B0B57FF00979990FEF987FBEE334FE67ED8C632C9E43BFAFC3F6A6528E3F33F67C7BDE51AD6B9AB7986F0DFEAF78D77385E10A901638631D238635016341D954019978E1180A0E34E6666CA9E97A4DE6AD2CB1DA88E386D93D6BFBF9DC456D6D10EB2CF2B6C8A3EF3D00276C33908B14B35BF39D9E976D71A2F92E593FD250C3AB79B594C57374A7678AD54FC50407A1FDB71F6A83E1C638CCB797C9203CB09005074197B25366FF6B15512DF7E2AA4CDFDB8A548B5314A9336155127EFC09522D8AA913FD9852A44E2AA64FDF8AA93361052A2C7EEC6D202993892C9498E0429138DA56E2AD13815693EF8A5AAE2B4B6BE27148689C556127155B5C556938AADA9C55A2714AC2702AD27C3156ABE38A792C26B8AB44D30256138156138156134C52B09C55613D862AB09A604AC27B9C79AAC26BF2C0AB49A6D812B6BE38AAD27155327C702A993E38A56135C09A584F8605584E2AB49C52B2B5FE38AAD269D3AE0E6AB7224A56135F9629584E2AB09A62AB09AE2AB09EC314ACAD302ADEB8A5693E18156E2AB49C556E2AEC55FFFD2E460FDD9E82F08A80E2AACAD8505581C50A81B155656FBB1555071410AAAD8B15456C50AA0FDD8AAA06F0C555037862AA81B0A15437FB78AAB2B1A7EBC50AA1B15A550D810AA1BFDB18A150362AAA1B15540D8A2950376C58AA038AAA06C555036285E1B15540D8AAF0D8A150362ABC36D8AAA72C55786C2B4BC360415E1F1452F0DFE63142F0D8A1503E295E1B142EE58AAFE58AAF0E7E7810B83E2B4BC3FBE2ABC3E285C1B155C1BE8C55772FA7156F962ADF2F738AAB4114D7522C36F1493CCDF6628D4B31FA0571BA4809B0D2E2B6DF55D462B223ADA45FE9171F228878AFF00B26191E2BE413C35CD78D4EC2D0D34CD2D19C74BDBF22E24AF8AC74112FD21B1E12799F926C0E480BCD4AFB5021AF6EE5B9E3F611DBE05F6541F08FA0648003922C9E684E5EDF761437CB156C36155D53812DF23920ADF2C2AB81F7C55BA9C55BE5E38AB7C862ADE2ABC1AE44AAE07B604AA74DF05A485FEF8AAA642D2A98AF45F90485C3C315541D46410AA3014AA2F8E0B555518095555191556031556E9B64121B030B20ACA2A428DD9BA28EA70334F61F2F6ACD10B89ED869D6A4545DDFBA5AC74F10662A5BE80720720E43766205C63F2E590FF004DD6A4D4651D6DB4B8495AFBCF3F05FA42B63EB3C857BD6E23ADA937996DADB6D1F42B4B523ECDDDED6FA7AF881201103F28F0F844F33FA17C4EE094DFEB1AAEAB4FD25A84F788BFDDC2EFFBA51E0B18A22FD032718463C831948CB994BC7E19250AA06C315550298A55147DF8A5540FBF012AA8053AFD38129C5C59D968D6F1DEF99EFBF435BCABCEDAC027A9A85CAF6315BD41553FCF2155F0AE4048C8D405FDCCC811DE46BEF609ADF9F6F6F2DE6D2B41B73E5DD1661C2E2389F9DDDD2FFCBD5CD14B03FC8A153D8F5CBA1800372DCFD83DC1A679C9151D87E399602001B0D80E832F684F2DF4CB6B7B05D73CC578747D098916CE143DD5F32F58ECE124733D8B9A22F735DB2065BD4773F77BD2C1BCCDE71B9D7214D2ECADD745F2D5B3FA969A242E5B9B8D84D7525019E53FCC761D1428CB71E3E1DCEE7BD980C289CB52A4CD8AA93362AA4CDF7629522D8A5458E29522D8AA93362AA4CD8A5489FBF155327E9C55499BAE2952271480A4C7FB3164A44FFB78AA9138AAC2715584FD38AB58A69C69DF14AD2DFEDE2AB7AF7C55693BFCB15689EE715584E2AB6BEF8DAAD2D812B49C556D7E8C534B49C5569F1C09A5A5B02AC2715584D70256938AAC27B9C55613E3D302AC27C314D2C27EFC0AB6B5EB894AD27C322859D315584E29584F7FBB1558C6BFD314AC27C70256138AAC38156938A5656B8A80B49A6C3AE479A56624AAC26B813C96938AAC3B62AB09AE2AB09ED812B7A62BCD61DF14AD27B6055B8AAD27155B8AAD26980956B91C16AFFFD3E3C0E7A0BC2AA838A1501FBB155553FED6152AC0FF00B78B1550D8AAAAB7FB58AAA86C55501A62C2A9543786285456C555037FB78AAA06C555036285656DBC30AAA86C5550378F5ED810ACAD8AD2A06FA3142A06C50AA1B15540D8AAA06FF6B1452A06FF006B16242F0D8AAA06C55786C50A81B155E1B15A540D8A1786C55786C2ABC37BE2ABC36055E1F1452F0D8A42F0F85852FE5815786C50BC3E2ABC37F99C55772C55772FA7155C1B02D2F0DF4E285E1F15A5C1B14236D6CEE6F391B788B469FDECEC42469FEB3B10A3EFC04D240B4681A4D9EF3CAFAACE3FDD30131400FBC84726FF006207CF1DCF92765B36B379244D6F032585A375B5B55F4D587F9647C4FF00EC89C4442DA5818780A6490E2DED4C52BEA69ED8A1B0DEF4C52BB97BE286F9614B7518AAE07DF10ADF2C2ADF2C2ADD7DF155DC8E2AB81AE2AB81A602ABC6D812A837C090178DC60555076C8DA42A0E98142EAEDF2A60B5E8A95C8A42A2F7C055557208541D3014AA81DB02AB28AFF1C0AAA2829BEE7A64553BB4D0758BC8C4D069D37D5875BB947A3081E26590AA7E39196488EACC449576B0D2ED37D4BCC5671B0FB56D621AFA5F9563E3157FE7A646C9E43E7B2680E654DB56F2E5AFFBC9A45DEA920E92DFCE218FFE44DBFC5F7C987824799AF72F1C474536F36EB080A69E6DB4442294D3A0485E9EF31E529FF83C7C28F5DFDEBE24BA6CC7E69A6BA94CF7334973313F14D2B17727DD98939681B31E6DAE055E3166A98A42F03A7E38A5554629550314AA8F138AA6F1696E966353D4EEA0D13486AF0D4AF4955908EA208C0324CDEC8A7DC8CACCF7A1B9FC7C9988ED6760C72FBCFD67A67287C9F66CB73D0F99750446B8F9DB5BFC51C3ECC4B3F815C98C065F59F80FD3DEC0E603E9F9FE393CD2E2EAE2F2E26BBBB9E4BBBAB862F7173339924763D4B331249F9E64015B06826F72B618A7B99A2B7B785EE2E2760904112977763B055515249F6C284DAFEEB46F26D57534835FF3427D8F2FABF3B3B26F1BE910FEF1C7FBE50D07EDB7ECE40033E5B0EFFD5FAD205BCAF59D6F54D7EFA4D4B58BD7BDBC9005F51E815117658E345A2A228D82A80065F18888A0CC0A4A0B64D544B6055227FDBC295266C55459B14A9B3629522D8AA93362AA25BEFC52A6C7DFE9C5548B62AA45B14A9B37FB58A40522D8A548B75FD78A5489FBB15532D8AD2CF99FA314B5518A5693F462AB49F0C55A276C556938AAD2D8AADC55A24604AD27155953DB15A689A7BE295A4938AD355A60B4AC26B815616C0AB09EF8A56135C556934C55613DCE055A4E295327C307256AB4C0AB09AE055B5C52B09AE285A4F6EA71480A44D3F8629584E0480B49F0C0AB09C556138AACC0520344F875C09E6B2B4C04A56135C50B09C52B49A62AB09AE2AB09F0C52B49EF815613538A56934E98156E2AB49A62AB7155A77180AADC695ADFC71B57FFD4E340E7A0BC32A034C50A80E2AAA0E285556185554362C554362AAA1BE8C55555B142A2B62821543628A540D8A150362ABC37FB58AAB2B628550D85550362AAA1B02AA06C282A81B142A06FF6B0215037BE2AA81B15540DEF8AAA06C5042A06C58D2F0D8AAF0DEF4C5553962ABC37BD7142F0D8AAF0D8AAF0DFE67142F0DF46155E1BE9C0ABC362ABC3781AE2ABC361452E0F81042F0FFE670AD2F0D810B83E285E1F14AF0D8A1706C55176F6B3DC02E8A1215DA4B990F08D7E6C7F50DF154609B4FB4FEED0EA530FF764954801F641466FA48F9604A85CDFDD5DF113CA5A34FEEE1501634FF55168A3EEC40421C3F8E28A6F9570AAE0DBF4C52DF2FA3D8E2ABB97BE2AB837D38AB7CBDB155C1BDF0AAEE58AB61B10ABB97BE495BA9C55BE58AAEAF7AE2ABC1C4AAF06B912AA8BE18121514F6C09E4AAB82D34A83A644AAA0E98AAE1D320BD15075C0AACBDF229545DC851BB1E8A373F760B54FE0F2EEB72C6266D3E4B5B7FF969BB2B6D17FC1CE507DD9599C5220571B2D1ED77D43CCB6808EB0582497AFF002E4A122FF87C6E4790F9EC9A03996BF4B796AD7FDE5D26F755907492FA75B78EBFF18A00CD4FF9E983824799A5B016FF008BF544A8D360B1D157A06B3B6412FB7EFA5F524AFF00B2C7C18F5B2BC67A6C935DDF5F6A2E65D42F67BE93F9EE24690FD1C89A648011E418937CD4C0A6142F1D314AF5E9F2C052A8BFDB8B20AC3A60485CB8A5500C590555C52A9D3E8C553A87499FEA8351BE9A1D1B49EDAA5FB7A51353A889685E53ED1AB1CACE4174373E4C8476B3B2457BE77D1B4AAC5E5AD3FF004B5E2EDFA77558C7A4A7C60B2A91F23296FF005064C6294BEA343B87EBFD4C4E511FA7E67F53CEB53D5752D6AEDEFF0056BE9B51BC7D8DC4EC58803A2AF6551D80000CBE31111405069948C8D94BF24C537D3747B8D422B8BC79A1D3B48B223F486B776C52DA0AF452C012EE7F6510163D864652ADBAAA53AAF9DEDF4F866D2FC96B359433298AFBCCD3009A85DA9D9962009FAB447F954F361F69BB618E2BDE5F2E9FB5988BCC891D3F0CBD929B362AA45B0AA916C52A4CD8A5459BC315532D8A5499B15522D8AA933629522715532D8AA9336295266C5952916C55499B14A9138AA9B1EF8A54CB6295B5AE2AD1C55AAF6C556E2AB49C55AAE2AB49C556924E04AD27B62AB7E78A79344EF8AF35B82D34B49C0AB09C6D56135C095A5BC315584F738AAD2715584E04D2CAFDF80AAC2493848D92D123E9C821613E3855613812B49AFF005C50B39531654B0B62BCD613DF0256138156934C554C9C556F5F9604AD2DD8629E6B4ED80A56135C0AB09ED8AD2D2698AAC26B8AAC27B0C52B7A605E6B0EF8A5693DB02ADC556938AADC55AEB815693BD31A5584D3012AD7238ABFFFD5E2CA73BF7875E0E4914AA0E285407FDAC55501C50A8ADE1854AB06C50AA1BEEC50A81BFDBC55543628540715A550DFED62C485E0FBE28540DE18AAB2B6D8AAA06C50A81BFDBC5554362ABC362AA8AD8AAA86C514A81AB8A1786C50A81B15540D8AAA06FA715540D8A29786C514BC378FDF8A15037D18AAF0D8A1706F7FBF14AF0D8A1786C5550362AB837D38A1786C2ABC36055DC8FF00662ABF9E2AB83E28A5FCB0AD2EE58114898229672444B50BBBB93C55478B31D8614145892CED8FC205F4C3F69AAB083EC362DF4D0605519AF27B9606690B85D910502A8F0551B0FA062AA61B142E0D8AAE0D5FECC55706FA3155C1B155C1B155DCBE9C55706C2AD86F7C55772C55772FA3155C1B156F96495757DF155C0E2ABC1A62ABEB4C16ABC1EF812AA3B1C169015875C8A55075C055501EB816D5EDE19AE5C476D0C97329269144A5D8FD0A09C893491BA7A3CB7AA42AAF7EB068F19A7C7A84F1DB9A7FA8EDCCFD0B95F880F2DD9F01EBB3462F2E5AFF00BD7AF3DEB8EB0E9B6CEE3DC7AB70615FA4038FA8F21F36351EA569D6F43B7FF783CBC6E9FB4FA95CBC9F23E940215FA0938F864F33F24D81C829B79BF5DE263B2B88B488FF00DF7A7431DB7FC3A2F33F4B63E147AEFEF5E33D364867B8B8BB7325D4F25D4ADD6599DA46FF0082624E48003931E6D2F6FA315575C8215D7129565ED91554FE38AAFC4242F5E9F3C09561D3148541D060641500FC7148545EC3B9340314A792695FA3E15BBF305EC3E5CB4907288DE57EB12AFF00C536A80CAF5EC6817FCAC871DED1DFF1DECAAB9ECC72EFCF7A6D8563F2BE92259D7A6B9ABAA4B257F9A1B41CA28FD8B973F2C98C24FD47E03F5B13940FA47CD806A5AAEA7AC5D35F6AD7F3EA576DB1B8B872EC07F2AD7651EC2832E8C4445014D2644EE503924368AEEE91C6ACF24842C71A825998EC0003724E2A9F5DC5A479517D4F3416BDD5E9CA0F285B49C65527706FA65AFA03FC85AC87FC8EB9104CFE9E5DFF00ABBD23779CF983CCFAAF98E584DFCA9159D982BA6E916CBE95A5AA1EAB0C43604F763563DC9CB61011E4CC0A6385BFDBC9A548B62AA65B15522D852A2CDEF8A548B629532DF462AA4CD8AA916C55489FF6F14A996F0C55499BC3155266C521489FBF16436522DFEDE2B4A44FDD8A54CB62AA45B1480A44E29598ABABF862AB6B8AAD27156B02B44D314AD249C55AAE2AB6BE38A5693E18AADF9E0B501A270256138AAD27C302AC27C714AD26B8AAC27155A4F8E2AB09EBE1812B09A6055BD705AAD27C315598156938A561F7E98AAC27EEC53C94C9DF0735A5A4818A56135C0AB49F0C556134C556FB9382D952D26B8392F3584D3FA604AD2715584F6C556134C55657155A4F86295A4D302ACEB8A56934E98156E2AB49A62AB7156BDF02AD26B8F355326A2980955B81900EA8F1C593FFD6E20ADFED677CF10AAAD850A80D3E59242A034C50A8ADE18A1501C55515A98AAB2B61452A06F0C50AA1BC3EEC555037FB58AAA06C50AA1B15A5E1862C5555BA628550DEF8AAA06C55503628540D855786C5550362AA8ADBE2AA81B02295037D38A1786C50A81BFCCE2AA81B155E1B15540DF4628A5E1B1452F0D8A1786F6C557F2A62AB837F98C55786C50BC362ABC3E2ABC36285DCB155E1BE8C2AB837D3815550B3B05452CCC68AA0549C55160416FF00DFB7AD28FF008F743F08FF005DBF80C51B95B2DDCB305424244BF62051441F203F59C5694C36DFC30AD361F15A5E1F1452E0DFE631452E0F815706FF00318A150362ABC362ABB96155C1BDEB8AB61B155DCBDF155DCB155C1B0855C1B0AAE06B8AAF07B1C2AA80F6C0ABC1ED915555F0C059008CB4B6BABD7F4ACEDA6BB93B2428D21FB941C8C881CD22CF24E8F97EF2000EA77167A30EE97B708B27FC894E727FC2E438C1E5BB2E0239ECD13E58B53FBED4EF35471D52C6DC431FFC8DB835FF0092782A47A009F4F7DAD3E60D3EDC91A77972D548E93DFC925E3FFC09F4E3FF0084C7C327994710E81427F34F982E23317E9496DADCF5B5B40B6B1FFC040107DF84628F7299CBBD24AF26E4DBB9EAC7727E9C2C578C52BC1A1C0ABC6D82D57AEF53E1815597B640AABAE28565EF914AB0EB81553150B81A8C592A8EC302AA8C5904759595E6A130B6B0B596F273D21850BB53C4815A0F7391240DCB202D1574341D12BFA7F5B8C5CA75D1F4BE379755FE5791584117D2E48FE5C00CA5F48F89DBF6A761CD8DDDFE61DDC1CA2F2BE9D0F9763DC0D42BF59D41878FD61D408EBFF1522FCF2C1801FA8DFDCC4E53D3660535C4D753497373349737331E52DC4CC5E4727BB33124FD39701428355A9E2ADD4F8E2A9BE9BA45D6A31CF75CE2B1D2ECE9FA4359BB6F4ED60AF40CF42598F645058F619132AF7AA06FFCEB69A3A4965E4A59229994A5CF9B6E1785E4808A30B48F7FAB21F1A990F765E9846227797CBF1CD908F7BCCD9C92CCC4B3392CCC4D492772493E397B3522D8AA916C52A65B15522DFEDE2AA45BFDAC29522C2B8A5489C554CB62AA45B14A916C55498E2AA65B155266C5214CB62C9459B14A916FF6B15532DF7E2AA4CD8A40522D8A56E2AB6B8AADAE2AD5702B5518A56924E2AB49C55A2714D2D2698ADB5D715A5A4D323695A4E2AB09C16AB49C55616F0C52B49EE7155A4D715584E04D2CAF738AADAE02AB7A6F82D2B0927E58A1693E1BE295A4FCCE0559CA9F3C569633629584E05A584E295A702AC2715595C55A3B6E702792C26B8392405A4D3025613DCE2AB09AE2AB49A62AB3155A4F8629E4B09A6055BD7AE295A4F6C0AB7155A4E2AB702B5D7155A4F6C1CD5489C4956B02405A4E29256E14517FFFD7E140FDF9DF3C42AAB7FB58AAB03850A80D3E58514BC1DAB850A81B15540D4C50A81B155456AE1452A83EF5C50A81B15550DFEDE2AA81BFDBC555036282AAA7162AA1BB9C50A8A7DF155E1BFDAC555036285E1B15540D8555036055E1B15540DD315540D8A29786C514A81B142F0D8AAF0F8AAA06C55786FF00318AAF0D8A29786FF338A085E1FDF142F0DF462ABC362AB836285E1F14AE0D8A1786C5511121705D98451034321FD407738A154DD71531DB8F4D1851D8FDB6F99EC3D862B4A21B155C1B155C1BE8C55772F7C55706DB0AAEE58A29786F7C0B4B8361452A06C514BC36042F0D8AAEE5855706F7C55772C55706F7A62ABB96155C0D70AAA035DB15568C348E23453248DB2A2824927C00DF12553D4F2F6A6A8B2DE245A4C2DB89B50956DAA3C4239E67E85395998E9BB3E03EE5DE9F972D7FDE9D5AE35290758B4F83827FC8EB8E3F821C1EA3C8526A2D1D7EC6DF6D37CBF6B191D2E2F99EF24FF816E110FF0080C782F995E21D021AEBCC9AEDEA18A6D4E75B73FF001E9091043FF22E208BF8622111D14CCA4E3C7B9EA7C72485406BF3C08540702578C5578DF22ABC1AE295E1B05AAE072255597BE02555D7B644A110BDB02AB2F6F6C8AAAE295E3718A42F1B52A698A53E8F44BD16E2FAF8C3A2E9CDF6751D4A416D1B0FF8AC37C727FB056C8198E4373E4C80EF4AAE7CCBE52D2EAB690DCF9AAF17A4B272B1B007D86F3C83FE45E48639CB9EDF69FD49E203CD8AEABE75F306AF0B593DDAE9DA5B75D234E416B6C7FD754F8A4F9C858E591C318EFCCF7962644B1614036A01E032C60BABE38D2B781552249669238618DE69A660914280B3BB1E815454927C062A9E5DA68BE56A9F3137E92D614563F2ADA494319EDF5EB85A88BDE34ABF8F0C80267CB61DFF00A9205B00D7BCCDAAF9864845F4A91D9DA5469FA4DB2FA5696CA7B4510D81F163563DC9CBA1011E4CC0A63A5B26C94CB62AA45B15532D8AA916C55499BFDBC295227FDBC52A65BDFE9C5548B7E38AA9138A54CB62AA4C715522D8AA996C5952916C521499B14A916C554CB7FB78AA9337FB58A42916AE29532D8DAADC55AAD3025AE5E18AADAFBE2AD57156AB8A56D7155BBE05A6AA060B4AD27155A4E2AB49AE04AC27155A4F8E05585BC3155A4E2AB493F2C6D2B09ED8156EE71255693E1815693F4E2AB49C09587C7155A5BC315A522714F35B812B09C0AB49A62AB49C52B2BE1F7E28689A6064B2A7BE02540584F8604AD2698AAC26B8AAD2715598AAC26BF2C53C96934C0AB3DCE29689F0C0AB7155A4D3155B8AB5EF8156935C0AA64D712556E0480EC59134B09AE2801AC593FFD0E0AA7FB0E77AF134AA0E142A86FBF155456FF6B0DA1541C2A5783FED6142A06FA7142A03F762AA80E28540DBE15A540DEF8A0AA06C50A81B15540DF462AACADB0ED8A295436282150362C5786FBF1554E5E38AAF0DF4E2AA81BDF155E1B142F0D85550362ABC362ABC3605540D8A29786F7C514A81BDE98A1706C5550362ABC37F99C55787FF00338AAF0D8A29787C569772F7C58AA72F7C569706F6C55706C5514A122A34DBB1DD61E87FD91EC3142D799A421988DB655E800F00315706C55706F7C55706C55706C50BB962AB8362ABF962AB837BE2AA81B155C1878FD38AD2FE585895E1BFDAC569786C50BF96286C37BE295E1BE9C508CB4B3BCBF7F4EC6D66BB93BAC485A9F323A7D389207348169A7E898ADB7D5756B4B023ED5BC6DF5A9FE5C21E401FF59860E3EE0CB87BDDFA4340B4FF007974E9F5594749AFA4F462AF888603CBEF931F51F25D834FE67D63818AD268F4A81B630E9F1ADB54781741CCFD2C71101D774F11092B3B48E6491DA4918D5A4624B1F993BE4D8B60ED8AF35C1B236BEF5E0F8604AF07DE9895E4A80E05540702AFAD31215783DF2295E0F438AAF06B82D553C722AAEBD7E780AABAF6C8A110BDB02AAAF7C8A534D3F4AD4B530E6C2CA5B88E2DE6B8038C318F1925621147CC8C89901CD205B73CDE57D22BFA575E1A85C2EC74CD140B96A8ECF74FC605FF00625F08139721F365B75492E3F306E6DEA9E5BD26D7415150B7EFFE997DF313CCBC50FF00C6345C98C17F51BFB02F177308BCBFBCD4AE1EF351BC9AFEEE4FB77373234B21FF0064E49CB80111418A1EA7156F90F963B2D2E07C3010B6BAB829539B1D225B8B67D4EF2E62D234589B8CDAC5D5446587EC42A3E299FF00C9407DE837C065BD73284B6FFCE91D8C52D8793E1974C8A4531DD6BF353F48DC29D88565A8B743FCB19E47F69CE118EF79331179E96EBE27739732A532D8AA916C52A65B15532D8AA916FF006B14A9B37FB58AA913852A65BFDAC5548B62AA45BFDBC52A65B15522D8AA996FF6B14A916C5952996C52A45B15522DFEDE2AA4CDFED62AA45B1654A64E2AA64D702ACE58A5AAE2AD57155B538AD358A5A2D8AB58AD2D26991B4AD27155A4D31B55A4E04AD2698AAC249C556D46055A4F8E15585BC30269693E38156135C3D15AA819155A4E055A4D315584D3BE295A4F7EDD862AB0B629584E0B5A584D314AD26B8156138AAD27C71559D7E580A405A4F618A56E4495A5A4E29584D31559D7155A4F862AB49A629584D7E58AAD27EFC0B4B71B4AC26BD3A6056B155A4E2AB7155BD6BBFCB012AD13DB15536C04AADC0901D8B2269613D462801AC593B1B57FFFD1E000FDD9DE02F12AA1B6C36AA81B0A1501F1C55555B155506B9242F07C7EFC285E188C514A80D7E78AAF07C7142A06A77C555037D38569541C58AA06C55555B6C5554362AA81BE8C5042A06C5890BC37DD8A1786C5550378E2ABC362ABC37FB58AAF0D8A150362ABF96155E1B02AF0DE07142F0D8A5786C514A81BC71452E0DEFF007E2AA81F142E0DFE6315540D8AAE0F8AAF0F8A297A92480BB93D00C569122458B65A3CBDDFB2FCBC4FBE2C694F9D4D49A93B9AE2B4BB9E2AB837BEF8AAF0DED8AAEE58A1706C52B836285E1B155E1B142F0D8AAF0D8AAEE5B62AA81ABDF155DCFDE985695037BE2A8AB5B7B9BC93D2B4B792E65EE9129623E74E9F4E02698D26474EB7B5DF55D4E0B423ADAC1FE953FC8AC6782FFB271838BB824456FE95D2AD4D2C34917320E973A8BFA9F7431F141F496C689E653410D75AE6A97C821B8BD716E3A59C548A11F28E30ABF86222029B297034DBB6498AA03F48C2AAA0E155E0F7C557D6BF3C052B81AF7FA32295E0E155E0D722AAA0F89C085E0F8E2ABC1C0ABC1A604AA03D302AA8C055597B605554EDF2C892A98595ADD5F4CB6D656D2DE5C37D98204691FEE504E449039A026B3D869FA41AF98B5CB4D2A45FB5A7427EBB7BF230C04AA1FF008C8EB9104CBE916CABBD269FCEBA3D8D5740F2F8B99474D4F5A613B7CD2D62E312FF00B32F931849FA8FCBF5A76629AB79975ED7B8AEADAA4F790A7F7366484B78FD9208C2C6BF42E591C718F214B692D69ED9256F91C55BE55EB8AAE07C0E2AB8378E0A422AD2D6E6FAE22B4B2B792EAEA6348ADE252CEDF203C3B9C89D9532BABAD0BCB755BBF4BCC9AE274D321726C2DD87FCB44C87F7CC3F9233C7C5FB620197904816C1358D7753D76E56EB54BB370F12F0B6840091411F68E1894048D47828CB23111E4CC0A49CB6492A65B15585B15522DF4E2AA65B02A996AE14AC2D815499BDF082AA45BEFC295266C09A522D8554CB62AA45B15532D8B2A532D8A40A522D812A45B0AA9336055266C52A45B14AC2715522DFED629595AEF8AB55C569AFA714D35503155B53F462B4D60B5A689C095A49C556938AAD270256938AAD26BF2C556134C556938AAD2D4C09A5A4F8E2AB0B786055B82D5AE5E18AADC55657025693DB155A5B6F7C5694CB7DF8A569270242C2702ADAE2AB49C52B09C50B6BDCE0259725A4F874C0AB4ED81216135C55613E18AADC556135F962AB49A629584D7AE2AB4B786055BEF812B49AFCB156B155A4E2AB7155A4F6EF8156E2156135DBBE02556E06402D2715256E2A03BA62C9AAE2AEE43157FFD2F3E03F76774F12A80F861055786FF6B256AAAADFED628540DF7615550D8AAA06C210BC1A74DC6142A038514BC362AA81BE9C55783E18A151587538AAF0DEFF004E142BAB6DE38A150362AA81B15540D8A15037862821786F1C514BC36285E187D38AAA72C557838AAF0D8AAE0D8A150362AB836155E1BFCCE2AA81B02AE0DFE63155E1BEEC514BF9531452F0DFEDE2ABC36285E1F15545AB1A0F993E1F3C555BD50A0AA1EBF6DFA13EC3C0628A5A1B14AF0D8AD2E0FF004628A5DCB1410B837BE285C18763F462ABC1F1C55706F7C55786FA3155E1BFCC628540DEF8AAF0D8A1786C55786DC01D4EC0789C2A9BA6993C68B2DFCB16990B0A8372489187F931282E7EEA646FB95DF5DD22D3FDE6B4935394749EEC98E1AFB4319E47FD93FD18EE7C9287B9D6B51BB8FD092E4C56DDACE0021847FB08C007E9AE2000A9686F0A7CB24ABC37DF8A1786DFC3155656FBF1554076C210A818FD1855783EF8AAFE55FE3914AE0DED4C0ABC1AEDD0E2AAA0FD1815501ED810BC1F1C557838292A80D302AA2F860557895E4748A3469257D923405998F800373912AC85F439AC5166D76F6D7CB9111555BF7A5C30FF0026D630F31FA540F7C8715F2DD3C252D97CCBE55D3B6D3B4DB9F315C2EC2EB5063696B5F116F0B19187FAD22FCB0F8723CCD27648751F3A798B5281ACDAFBF4769CFD74BD3916D2DC8F0648A85FFD996396471446F4B6C60103619342EAE2AE07C7014AE06B815BAE2AB81C55BAE2A9E41A6470DAC7A9EB776347D2E415B7765E771734ED6D05417FF58D10776C813676549753F36CB25BCDA66836E744D2251C6E42BF2BBBA5FF009799C50907F91689EC7AE4843A9DCB20186F2EC3603B0CB12B0BE29532D8AA996C554CB605532D8A5616C5548B62AA6CD8AD2916C29522D8A54CB0C2AA45B15532DFED629532D8A40522D8192996C5548B62AA45B14A9B37BFD38A6948B62AA65BC3155363F862AA64E29A6AA314ECD72C55AAE2B4D5698094ADE5815A27155A48C55696C095A4E2AB4938AAD240C556927E5815654629A5A4D71B55A4D3155B5F1C0556923B6055A4FBE2AD13812A65BB62868F4DF14AC276F6C53C94EA702D344D314AC2702AD2698AAC27155A4D7A629A5A4D3E7839A56135EB809501A269812B09EE715584D715689A62AB09AE2AB49A629A59EE702AD27C315A5BD30256135C55D8AAD27C3155B8AAD269F4E02556577F9E05689A624AAD3E381900B09C5495B8A80EC59344D315598AB54F738ABFFFD3F3C06F96774F14BC1EF850A80EF8AAA03BE10AA81B0AAA03F7E142A06C555037D1850A80FD18569783E3B7BE142A06C50BC37DF8AAF0D8A17838AAAAB74C282AA1BE9C55503628540D8AAF0DF4E2AA81BE9C50BC37F99C514A81BE8C514BC353142FE58AAF0DF4E2AB837DF8AAA72C55706C557F2C50BC37BE2ABC36155C1B02AA07F7C50B837D38A57F3C514A886A6B5A01F69BB0C50550CA29C5765EF5EA7E78AD341FDF155E1F142E0F8AAF0F8AAF0FFE63155DCF155C1BE8C514BC37FB78A29786F0C514A81FFB7155C1B142A06C551104535CB88ADE269A4FE44153F33E18AA3BD1B2B535BEBB0F20EB6969491BE4D21F817E8AE36AEFD352C20AE9B0269ABD3D64F8E723DE66DC7FB1A60AEF5A4ADA5676691DD9DDCD5DD8D493EE4EF855DCF14B61F142F0D8AAA06C285407FDBC555437D18AAA86FED1842AA03F7E155D5F6C055783F46055453815783B781C55541C085407B6055E0F8E2ABC103A9C0529EDBE83A9496E2F2E523D2B4F6DD751D4645B5848FF0020C9467FF600E40C87BD2225465D4FCA5A65419AEBCCF74BFB16C0D95983EF2C80CAE3E48BF3C78647C93412BB9F3DEB6C8F069460F2E5AB8E2D0E969E8C8C3C1EE18B4CDF4BD3DB24310EBBADB1167677691D8C9239ABC8C49663E249DCE4D0D72C52B81F7C55772C55BE5EF81575702B75C0AB81F7C551B636579A8CFF0057B280CF28059E940A883ABBB1215547724D30134A889F56D1F41AA588875FD617ADEBAF2B0B76FF008AD1A86761FCCDF07806EB8384CB9EC19530ABFD46F753BA96F750BA96F2EE6FEF2795B931F01EC076036196015C92812D852B0B75C554CB62AB0B605585B14A996FF6B15585B15522D8AA996C52A45B0A548B62AA65B15532C7FAE29532D8A40522D8A42996FF006F14A9B362AA45B155266C52A45B14A993DCE2AA65B14A9938AAC2DF7E295BD715A6B05A69AA8C55692702B55C6D56F2C095B538AB448C556124E2AD12062AB49C556920605584938DA696934C0AB6A4E36AD134C0AB49AE055B852B49FA302ADA8EB8AAC2D5E9F7E29A584FD27155870256938AADC0AB49F0C556134F9E2AB6B8121A27C305279ACF9E24AD2D27C30256938AAC26B8AAD2698AACC55696F0FBF14AD3B605584D714AD240C0AB6B5C55D5A62AB09AE2AD6055A7A5312AB09A62AA7D7012AEC0C805A4D76C5495B8A80EC59344D315598ABB1575478E2AFF00FFD4F3886CEE9E2D5437D38A29541C50BC11DF0A5783F778E4815540DE38A15037D3850A81BE9C55503628540DF4E1B55E0F87DD86D697823FB30A1786C50BC1F7C55554E285E1BE8C569555B0A15030F1C55786C50BC37D38AAA06C55786F7FBF142A06FA315A5E1B162BC37F98C55786C50BC37862ABB962ABC37BE2AB837D18AAF0D8A1786C55706C55786C55514D77268A3A9C557992BB0D94741FD714383F8E295E1C9C514B837BD315A5E1F1452E0DEF8AAF0D8A1786F7FBF155C1B15540DF462ABC362B4A81B1452260866B962B0C65F88ABB74551E2CC761F4E2C4A2B9585AFF007B21BF9C7FBAA2252107DE4EADFEC47D38AD5A94DA9DCCC9E8865B7B63FF001EB00E087E606EDFEC89C5785061A8314D2EE7EF4C50D86C55BE78AAF0D5E98A1503629540DE277C50AA1B0AAAA9FBB0D21541C34AA81B012ABC36055E0FD38AAF07C302AA82312AA80D722A99E9FA5EA5AA72FA859C9711C7BCB700718631E2F2B108BF49C0480902D132FF008734CAFE94D6BF485C2F5D3F47026DFC1EEA4A443FD887C7D47904F0A5D2F9DA5B6AAF97F4AB5D0C0D96F987D6EF3E7EB4C38A1FF51171F0FBF74DD72629797F79A8DC35DEA17735F5D3EED71712348E7FD93127240572421C37BE155DCB02B7C87CB155C1BE9C55BE58AAF07DF156F960DD5772F7C0ADF21DF02A702C6DAC218EF35EB86B08245E76F611806F2E01E8510EC8A7F9DF6F00D82EF924048B54F325CDFC074FB48974BD1C10469B013FBC23A34F21F8A56F9EC3B019211ADD900C6CB7F98C92AC2DF4629585B15585BDF02AC2D8A5616FA715532DEF8AAC2F8AD2933E29532D8534A45B155366FF006B15532D8AA996C534A6CD8B2A532DFED629522D8AA996C5548B7FB78A54CB7FB78A6948B62AA65B14A996FBF15585B15532DF778E0B4ADA8C1696B91C556D7156AB8EEAB49C09689C55696F0C556935C55692062AD127155950302696924FB636AB6A06255A24E44955A4D3155A4D702ADAD30A5693EF8156D6A7142D6DBDF148584E295A5ABED81696546295A4D702AD2698AAC26B8AB55C52169A0C09E4B09AFCB0725E6D134C09584938AAD2698AAC27155A4F862AB49A629584D7E58AAD2698156D6B8A5693E18156E2AD57155A4D7155B8AB44D3015584E3C9561C055AC0C805A4F6C5495B8A80EC59344D315598ABB155A4D7156B05A5FFD5F3686FA33B978C5E1BEFF1C6D0AA1B0A29503628540698557835EF8DAAF07FDAC92AA06FA31452A06C2ABC37D18AAA06C6D0A81BC70855E0FD230A29703FED6142AAB7BE2AA81BC7142F0D8AAF0D8AAA07C28A540D810BC36155E1B155E1BFCCE2AA81BE8C55786C5042E0DF4E28A540DFE67142E0DE27142E0D8AAA06EDD7155C1B155C1BE9C55786C55514ED53B0F118A1BF52BEC07418AAE0DEF8AAE0D855786F7C0AB837D18AAE0FEF4C5578618AAE0DEF8A29503628A5E1B142F0D8AA2218E59DF8C485C8156A7403C493B01F3C5515CACEDBED9FAECE3FDD684AC4A7DDBAB7D1B7BE28DCA8CF7D3DC008EE1615FB16E802C63E4A36FA4E2A0521C3FBE295DCF155C1F155C1F156C3E2B4BC37FB58A29786F7FA3162AC1ABFD7155E1B0AAAA9C50AA1B0A1554E2AAA1BE9182D57838AAF07C302AB46AF23A4712B49239A24680B331F0006E7154F9B46362824D7AFEDF4142390B7B8264BB61FE4DB47CA4FF82E23DF2377C9970A0A4F326896151A3E907529C6C351D5E856BE296919E03FD9B37CB1E127994D00C7B54F306B3ACF15D4B5196E618FFB9B4148E08C7F910A058D7E819311039294A39FBE250DF2F7FA702B7CBDF156C37BFDF8AAE0DEF815772C55772C55772FA7156F90C0ABB97D38D2A36CED2E6F9D92D92A231CA7958848E341D5A47340A0789C8934AA936B561A47C1A471D47515D9B58952B0C47FE5DA271F111FCEE3E4BDF1A279B201885C5CCF7334B717333DC5C4CDCA59E562CEC4F72C7739364A05B155A5F02A996FA714AD2D8AAC2DF4E2AA65B15585F0AA997C09A532F8534A65B15532D8AA996C554CB7FB58A694CB629A532D8B2532D8AA996C5548B7FB58AA916FF6B14A996C52A65B14A993F762AB0B62AA65BEFF000C09584E04D2C26B8DAAD2698AAD2DE1812D571B55B518AB5CB155B8AADE58AD35538AADA8C0B4B6A4E3695B503155A49C16AD120604AD27142D27156AB815613ED8A5AE54EB8AD2C27E818A5613E18156D714AD27C302ADAF8E2AB49F0C5565714ADDCF5C0B4D134E98AFB96EF82D21616F0C095A4E2AB09AE2AD134C556124E2AB49A7CF14AC26BD7155A4F8605A5B812B49F0C55AC55A2698AACC55A2698AAD276A60E6AB09ED8F255A3AE452D62CAA9693F762C495B8A40762C9693F7E2AB715762AB49F0C556E2AEC55FFD6F34039DC3C62F0DE38AAA0386D14AAADF76142F07155407142F070AAF06986D5786FA4615A540DF4E142A06C55786F0FBB15540DEF4C6D0BC357256AA808F1DF0A08540DFED6285E1BE8C55786C55786C50BC37D18AD2F0F8A295036142F0DF4E055E1B0AAF0D8AAF0DFE6315540D8A1706FA315A5C1B1634BF97BE2ABF96285C1F155EA7B93B62ABF9D7D876C55B0DEF8AAE0DF462ABC362AB836285C1B155C1BE9C55786C55786C55786FF006B15554E4ECA88A59D8D1554549C50519C60B6DEE5BD497FE59633D0FF0096E361F21BE2C772A72DECB32FA429140371046289F4F727DCE29114386C534B83628A5DCB155DCBDF155DCBE58A1BE78AAF0D8AAA86FF006F15540D8A0AA86C28540D8A1541DF155456FBB0AAAAB628550702132B0D3AFF0052661656CF32C7BCF36CB1463C5E4621147CCE0269205A29DFCBDA67FBDDA8B6B172BD6C34B20440F83DD4838FFC02B7CF06E5970A027F386A223783488A1F2F5B38E2C2C6A2775F092E58994FC8103DB0880EBBB21B3162E5999D9B9331AB313524F893DF268A6B97F9D702D3B9FBE36B4EE5EE3014B7CB0229786C569BAE286C37BE05A5DCBDB142F0DEF5C55772C52BB97BE2A998B7B7B2892EB5891ADE29072B7B18E9F599C788076453FCCDF40391BBE4B4926A5AE5CDF462D6344B1D3636AC5A7435E15FE6909DE46F76FA298631A66024C5FDF0A5616F7C55696F7A6055A5B15585BE8C2AB0B7F99C554CBE05A585F0A54CB629A532D8AA996C55616F7C554CB7BD314D2996FA31480B0B629A532DFEDE29532DD7F5E2AA65B155266C52A45B14A996FA314ACE5E18AA9B37DF8AA996EE71B5A585B23690B0B7FB78A54CB604ADAE2AB6A3156B962AD5702ADA8C2AD16C0AB49F138556F2F6C09A689270A56D46450D54E36AB49A6055A5B02B5855693812B4B5315584FDD8A406B97618AD2C271B5595C09A689C0AB09AE2AB49C556938AAD26BB0C556F4DCE0B4F25A4D7DB014F35A48182D405A4938A56934C556935C55693E18AACC55693E18A56E05584D7DB14D344D302AC26B8ABB155A4F862AB6B8AB44ED5C0AB09AE3CD5A276AE3C954F2290E2462CB92C26B8B1BB6B16403B14AD27B62AB71577E38AAD26BB62AB71577BE0255AA8C1655FFFD7F3183F4FBE770F1AA81BE918A1786C55515B0AAF56ED86D0AA0FDFE18A1786C50BC1F0C55783DC7DD92055783F461B5540D8DAAF0DF4E142F0D8AAA06C55555B6C210BC13F3C368A5E1B0A1786E98AAF0DF462ABC36285C1BDF155E1B15540DF4FBE2B4BC36285E1B142F0D8AAF0D855706C55786C55786C50B837BE2B4BC378E2C4AFE75EFF00218AB7CBC7142E0DFE67155DCB155E1B155DCBDF155C1B155FCBC3155E1B142F0DF462A8B8E2A209677F4223B824559FFD45EFF3E98A1735E95531DB2FA119D99AB591BE6DFC062BC3DE850D4F6C52B837BE2ABB97CF155C1B155DCF14361CFB6296F9FBEF8A29786C5695037FB78A15436285556FF6B15550D8AAA03EFF004E10C695437E3D46142A06A6FDB0DAA736BA55DCD0FD72531D869E7AEA178DE9447FD427773EC809C89905A73EADA0E9DB59DB3EBD74BFF1F3741A1B407C56153EA3FF00B261FEAE477291149351D7B54D542C77B76CD6D1FF00736318115BC7FEAC28020F9D2B840019255CF15773C2AEE78ABB9E2AEE7855772C0AD86C0ABC37F98C55786FF338AAEE58169703EF5C557038B1445BC13DCC822810C8F4248E8028EACC4D0003B9380A4AACDA9D9E97F058F0BFD417ED5FB0E50447FE2953F6CFF94C29E03BE0A25205B1A9AE65B89249E791A69A53CA596425998F8927274C94797BE2AD16F6C0AB4B7BE05585B15585F15585FDF14AC2F852A65F15585B15532D8AAC2D8AAC2DEF8A69616C534A65B1654A65B15532D8AA996FF6F02A996C29532DFEDE29522D8AA996C55696F134C554D98FD1812A64FD3812B09C6D34A65BFDAC09532D8AADA9C55AC556D46056B9615689F1C0AD54614D2D24F6C0AB76EF8EEAD72C0AB0D4E3697569DF0216D4E2AD6055B5FF006F14AD2715689F7FA7155A585298AD29938A569381696920604AD27155A4D3155A4E2AB09C55AC05202D2DE18D279ADF7C16AB4B78604ADC55693E18AAD2715584D7155A4818A56935C556934C0AB7AF5C095A4D3155BEF8AB47155B5C556E2AD74FA705AAD27155326B80956B0240B689A62CB92CC58F3762C8076295A4E2AB715762AB09AE2AD62AEF9E0B5584D710AEAE14DBFFD0F2F83E19DB5BC6F2540476C2AB836155407EFC50BC362AA81A98514BC37D1EF8555036285E1B155E1B0A1783E071B55E1A9ED86D5786FA30AD2FE5850AAADB75C55503636854E55C95AD2E07C37C50BC36142F0715A5C1BC7155E1BE9C55706C50BC31C55786F7C569707C514A81B142E0D8AD2F0DF46155E1B155E1BC7A62ABB9E2ABC3628A5C1B15A5C1B1452E0C3142FE58A1706C55706C55786F1C555A35791B8A2963DC787B93D862A88F561B7FB1C6E261FB67FBB5F903F6BE9DB1455A83CCF2B17918BB1EAC4E29A6B9E2AB837BE28A5DCF156F962AB83FBE2ABB97CB156F97CF155C1B15540DD0E2AAA1B142A86C50A81BFDBC55595B142A86DBDB154E22D3255892EAFE68F4AB27DD2E2E6A1A41FF0015440177FA053DF0993158FAEE9D61F0E8F63F589C7FD2D350557607C63B7DD17DB97238372C84521BCD46F35198DCDFDDCB7939DBD4958B103C057A0F61B62CA90BC8E15A6F9E057731E38A29DCFDEB8569DCFE78AD37CFE78AD383FBE2B4BB96285C1BE8C095E1B142E0DF4E2ABC36055E1B1546450A0885D5E4BF55B3AD164A55E423F6624FDA3EFD077C16A80BCD59E78CDA5AC7F53B026A605357948E8D2BEDC8FB741D8620240A49CB7BE4992DE58AB5C86255A2F9142C2FFE6314D2C2F8534B0B62AB0B7BE2AB0B62AB0B7BE2AA65B14D2D2D8AD2C2DF462CA9616C534A65BFCCE2AA65B15532D8AA997C52A65BE9F7C52A65BE9C554C9C52B0B7BE0B42C2DF46369532DE2705AD2C2DF760654B0B62AA65B14A996FA31559CBDB155B538ABABEF81569230A69AE5ED8156D7C4E3696AA307BD0B6A70A5D9142D27155A4938ABB155B5F7FA302AD27E9C52D75F6C556134FEB8A69696FA715584E05A689A6295A5B02ACC55A2714AC27142DA9ED8134D1206DDF15B5A4D705D2405A4D30256935C556934C5565715689F0C55662AB49F0C52B702AD27C314D2DC0AB49F0E98AB58AB44D315584FD18ABB1559535C16AD16C55656A69D305AADC0C805A4D314934B716205BB166E2698AAD2D8AADC55D8AAC26B8AB58ABBA6F8095584D71015AC4956E983895FFD1F2D83E19DABC7AF06BF3C6D14A81BC76F7C9217570AAF07142F07EEC55786C2AA81BFDAC368540DF4628A5407FDBC50B81F0C55783E3F7E155C0FD386D5786C212AA1B6C58AA06C2ABC37D18AAF0D8DA15036155C0F81C28A5C08C2ABC378628A5C1B155E1BDF155C1BC7142E0DE0714AE0D8A1786FA715A5E1BC31452A06EE7142EE75FE98AAE0DF462ABC36155C1B155C1B02AF0F855772FA31452E0DF4E28A5C1BFDAC508A44F87D495BD388F43D59A9FCA3BE286DEE6AA63887A50F741B96F763DFF00562B4A21FE8C55772EB8AAE0D8AB7CF155FCF156F97B8C55772C55B0DEF8AD2E0D8A15037FB78AAA06FBF15550D8AAA2B628550D8A13686C1FD25BABC9534EB26DD2E27A82FF00F18A31F13FD029EF8DA1CDAE5AD8FC3A35AFEF47FD2D2ED55E5AF8C716E91FCCF23EF8293C3DE914F773DD4AF71753BDC4F26EF34AC5D8FCC9AE14D2973C52DF3C55DCFDFF001C55B0FEF855DCBE78AB83E2ADF3C55AE5F3C55706C50BB962AB836055E1B15A5E1B142A29248500B1634006E49C0A8B924B7D3BFDE95173783ECD957E08CF8CA477FF00247D3839A00B496E6F2E2EE5335C4A6472283B0551D1540D801E0324032AA42F3FF3AE296B9FBE2AB79E04AD2F8AD2D2D8AACE78AAD2F8AAC2D8AAC2D8A56F2F7C569616FF006F14D2C2FEF8A69616C52B0BE2AA65B15585B15532F8A694CBE2B4A65B14AC2DEF8AAC2DEF8AA996FA7C0E455613DCE369585BE8C0C9616FF6F15532DFEDE29532D8154CB7D38AAC2DE270AD35C8629A6AA705AB55C56D6D460A56AA71D95AFA705A5AA8F1C50D72F0C55693E3815AAED5C295BC8E05689C556D7C7618AADE54C534B09F7AE2AB6A702D355C52B0B6056B155A4E2AB49C556D714B55A604F25A49ED81696E026D42D2DE18A56934C556135F962AD5698AAC26B8AAD240C52B49AE2AB6A302AD249C52D1206055952715762AB7962AB7156BDFF0C0AB4B63CD567218F255A4D70156B03301A2698A09595C5003B166EC556135C55AC55A2698AAD3BE2AD62AD1E9BE0B559880AEC4956F229E4D7D38ADBFFFD2F2B839DABC7AF0D8AAF07105578247BE1B634BC1F0C2AB81C285E1BE8C55786F1C55501F0C2169786C285E1B0A295036055C0F86155FCBC7142F0D8415540D86D5786FA7142F0DF46155E1BE8C55786C6D55037BE142E07E8C36B4B831C285C1B142F07FCCE2B4B8378E2ABB97BE2ABB962AA80FBED8A1BE67FA62AB8362B4BC3E28A5C1BE9C55786F7FBF15A5C1B142F0DEF8AAE0D855786AE2A8A1C61DE41CE4EAB09E83FD6FE98B1E6A4F33C8C5DDB931EFFC062901C1B15A5C1B15A5DCB14537CBE8C50DF2F7C55706F0C50BB962AD87F7FE18AAE0C715540DE18AAA06C55541C50A81BFDBC5530B6B49674698B2DBDAA1A497731E318F6AF563EC0138A09543A9D9D8FC3A743F59B81FF4B0B9504291DE284D40F9B54FB0C5157CD289EEA7BA95A7B999E799FED4B212CC7E938B20294B97BE2ADF3C55BE58AB7CB15773C55DCF156F97CB15772C55DCB156C362AB836155E1B155E1B02AE0DFED62A8886392662A807C2393B934551DCB13B018A0BA5D463B6531D8372948A497F4A1F71103F647BF53ED8294049F9FCFE78592DE55C55AE58AADE7EF8AAD2D4C55697F7C0AB7962AB0B629A5A5BDF15A5BCB1480B0B7BE29A5A5B14AC2D8AAC2D8AAC2DFED62AB0BE2AA65FDE98A54CBFF0099C534B0B62AA65B14AC2DDB15585875EB80942D2D82D900A65B05AAC2D8A54CB7D38A5616C554CB62AA65B14AC2702B551815AAF863B2ADE46B84A5D5C8A1AA81815AE58AAD270AB5815AA8C55A2714ADAEFD314347E7F46295BCBB0C534B0B7DF8A56935C0AB6B8AB44E2AB702AD247F6E2AB49C556D7C37C52D7CF05AD2D2DE1813CD6E369A689C0AB3155A5BC3155B8AAD2DE18AADC55696F0C534B702AD2DE18AD2DF9E295A4F86056B156AB8AAD27155B5C55A6DBBE0B55A5B6C55616C04AB47025AC59552CA9AE2C49B7135C5900D629689A62AB0EF8ABB155A4E2AD60E6AD61568ED91BB55A77C900AD60255BC8A561384042DC92BFFD3F2906FA0E76AF20BC378E285E0FDD8AAF0D8AAA03E1841410B8378E142F07E9C2AB837D38A1783E18AAF0D8AAF070A29786FF33855786C514BC362AA81B142F07C0D30DAAF0D86D5786FA70AAE0DEFF46142F0DF462ABC362ABC37BD7142F0D86D5786C6D5703EF86D14BB96155C1BC3F0C50B81FBB155DCFE8F6C55706FA7155DCBC7155C1BC0E2AB836285C1FDF15A5E1FFCC628A5C1FDF15545249014124F403BE2844FA820D94833777ECBF2F7F7C51CD479FB9DF14AE0D8AB7CB155DCFDF155C1FDFEEC55707F7C2ABB9604537CBDE98569772F7C569772C505783FE67162A8A6B8AAA838AA2608A59DFD3890BBF534E807724F403DCE2828B33D9596C42EA3743F6413F5743EE450B9F9507CF146E52EB9BEB8BB70F73299388A4694A220F05514007CB14814A01FDFF1C52DF2F7C55BE78ABB9E2ADF3C55BE7E18ABB97CF156F97CF15772F7C55DCFDF156C37BE2ABB962ABC3628A5C1B155E1BC0E2A888D03219657F46DD4D1A53DCFF2A8EE71B421EE6F8CABE842A61B506BE9F7723F69CF73F80C5202079629585C62AD17C55A2DE0715A5BCB6C09A68B7BE2AB39629A5BCF14D2D2D8AD2D2FFEDE295A5BDFEFC55616C0AB4B7BE15585B15585F14D2996FF00318A5616C554CB62AB0B636AB4B789C8DAAC2705AAD2D8A40585B1654A65B1501616C52A65B15532D8AAC2D8154CB629584E0B56B012AD6056891855AE5E18AB553E3815D855693E1812D57156ABB78E2AB6BBF80C55DC862B4B09A9C52B6B82D5693F762901AC55613815AC556D7155A4E2AB4B62AD7CF05A69696A74C556F5EB82D34D134C095A4938AAD27155A4D7155A4D3155A4D7155A4818A56935C0AB4903155B53E34C52B49A7CF155A493FD302BB155A4E2AB715762AB6A3A60B5584E2AB49A8C04AADC0C8068F89C5374B09AE2C79BB164053B14AD27155B8ABB155A4F862AB715762AD1391E6AB324AEC04A5D915B5A4D724021AC04A69D8114FF00FFD4F2683E39DA3C82A038557838AAF07142F0D8AAF0D8AAE07C30DA2978607D8E142EE5855786FBB142E07C0E2ABF962ABC361452E0D86D5595BBE285E181C50B837BFD18A5703F461B42FE5F4E10557063D8E15540D8DAAEE58A1786FF006B0AAE0DF462ABC3636ABC37BE3685E08EFB6482D2EE5E1F76368A6F96155C1B1452E0C715A5C1B156C3786285FCB14B61BE8C55514962140E4C7A0C50AE641102886AE7679076F65FEB8A294B9E29A5DCCE2B4B83FBE285C1B155C1FDFEFC569706C50BB97BE2ADF2C55773C55707AE2AA8ADFED62AA81BC30A2950118B1A4688A38544B78E61522A902EF23FC8761EE71473E4A536A124B1FA31016D6BFEF84FDAF776EAC7E78A44507CBDF1654DF2F0C514DF2F6C569DCBC714537C878E2B4DF2F7C55BE67C6B8A1DCFE58ABB9629A6F91F10315A6F99F6C50D733FED62AB8362ABC362ABF97D18AAE0DF4E2A891E9C2826B8AF16DE2801A33FF45F7C50819EEA4B860D2101545238D76551E00629010FCC78E29A773FA715A6B97B62B4D72F7C534B4BFBE04D2D2C315A68BE29585F155BCF155A5F155A5BDF15585F155A5F14D2C2FEFF00762AB0BE29532F8AAD2DEF8A54CB62AD16F7391B42DE580A5616C569616FA3164B0B62AA65B025616C554CB62AB0B62AA65B1E69585BE8C0AB390F9E02AD723815AA9F1C2AD605757156B9615772C095848C55DF862AB6A3E7ED8A69AE447418AAD2D8AAD2702D2DC52D546055BCB155B5C55AA818AAD27155A4FD38AB5F3C16C8068903142DA938A69A2698094AC24E055A4D3155A4E2AD134C556138AB58AAD2DE1F7E295B815613E18A69ADB155A4F86056B156AB8AAD27AE2AD62AD5682B8156F2C55693DF1E4AB3E7812D60480B4B7862A4B5538A80D62C9C715584E2AD62AEE98AAD27155B8ABBDF0134AB49ED8F355B855D912521C481BE042C26B86A95AC6D21D812EC55FFD5F2503E39D9BC8AE07EEC50BC1EE30AAF06B8AAF0DE3850BC1F0C55706C55783E38AD2F070DA005D5F7A1C2A57570B15E0FD38AAF07C0E2AB8378E2AA8ADEF86D0550377E9850B8362ABC376AE2ABC1C50B8378E155C0FBD70DA5706FA31B42F0DB6495786F7C0AB837D1850BC362ABB9F4C55706C6D5786F7C285DCB0DAAE07DF1B452E0D8AD37C878FDF8569706C514B81248502A4F418AAB971182887E23B3C83F50C514A7CB14AE0D8A1B0C314AE0DB78E2AD86C55773C514BB9E2B4BB9E28A5DCFDF156C37F98C55503628540DFEDE2A88895E56213B6ECC4D140F124F4C504AB1BA8ADB6B6A4B377B961F0AFF00A8A7F59C5157CD04D2B3B17762EEC6ACC4D49F99C593B9FCF156F9E2ADF2C55BE78AB7CF155DCF15773F9636ADF3C368A7731FE6705AD06F90C5683B98F7C2B41DC860B5E16C37BE15A5DCBC71452F07C3142F0D8A110D225A80D20124E455203D17DDFF00A6051BA5F24CF2BB49237276EAC4E2CEA9672F9614B45F156B9FCF02B5CF0AADE7815AE78AADE78AAD2D8AADE78AADE7EF8A696F3C55697F7C52B0BFBE2AB0B62AB4B7BE295BCB05A1696F7DF1B4AD2C32368585B14D2C2D8A405A5BDF14D2C2D8A54CB605585B0AAC2D815616C52B0B60B5532DFEDE05585BE9C556D7DF14B5810D5714BB90C50B796FD7E8C52EC556927E58ABB9789AE2B4B6A77DBAE29A5A4D3155BCB6C6D2D54E056AB8AAD2D815AA9C556934C55AE58AAD27C7155BCB156BE6705A69A240D86F8AADA93D705D269A269812B49C556D7C7155A4F862AB7155A5BC3155B8AAD2D812B7DCE2B6D138AD2DEBD714AD2702ADEBD715762AB49C556D7156ABB57156B90C16AB49EF8AA9D6B8095774F7C0902D6934C59725B5AE2C49B6B1480EC59355A62AB49AE2AD62AD138AADC55AC55D8095584D7E5880AD61B56F224A5A26982AD0561DF25C95AC1690EC097134C55AE43157FFD6F2303E39D9BC8AE07C3155E08FA7155C0F8E1452F0DF4E2ABC1C285E0F8E2AB81A62ABC1F0C557D71B55C0FD3858AE0461B480B81C28A5C1B142A03B7B62ABC37FB58557F2C6D0B83570AAF0D8A297F2FA3155E0E155C1BE9C086C11E34C95AAF04FCF05AAE0DEFF007E4AD57F2FF318DAD361BDF0AAF0D8A1706F7AE2AB837D18AAF0DF4E3685C1B0AAEE58AAE06A4015A9E830DAAB73E00AA9058ECEDFC06368A59CB156F90F1C285C1B15A5DC8E2AD86C55706F7C50BB962AEE54EB8A5772F7C55BE47155E1B142A06FF6B1422902AA892625233F657F69BFD51FC71429CB74D2008A3D3841AAC4BE3E24F738A4051E7EF8AD37CFDC62AB83E2ADF3F9E2B4DF3F7C569BE7F2C514DF3FF3AE2ADF2F9E2ADF3C55DCF156F9E2AEE5F2C55DCBE58AB61B155E1F155E1B155E1AA40EA4F4031410155A616BB0A35CF8F511FF0053FAB14016802E49259B931DC93B9AE2C9AE7EF8ABB9E2AD73C55A2FEF8AADE78AB5CBE78AAD2F8A5AE7EF8AAD2F8A69696C569697C556F2C55696FA7155A5E98AAD27DF236AB79636AD16C09A585B14D2D2C714D2C2D8169616C52B0B615585B02AC2DF4E15585B2369585B02AC2DF462AB0B7FB7815613F4E14AD2DF76055B5C55D8A1BC5569F0A8C6D34B4353B629A77338AECB0B62AD72C1696AA7C7156B6C55AA8C556D702B58AADA8F9E2AD72C52B49A6286ABE18AB5F3C169A5A5BC314DADA93D7012A03B6182D42D2C714ADC55696F0C556E2AB7962AB71568B018A696D6B815692062AB4927E58A69AD86055A4D7156B15689C556D7DB156B156B155A4E479AAC2DE1855660576D81900B49FF006F14934B7163CDD8B3A762AB49C556F5F7C55D8AB44D315598ABB15689A6D82D56D71015AC6D5D912534D134C42ADEB92E48764534D629712062859D714B58ABFFD7F2103F46764F22BC1A6155C0E2AB81F1C55783E18AAE070A1786F1C2ABC1C55703F46285C0E2ABC1F038AAF0D882AB81386D14B830F96142F0DEF850BC1C55703EF5C55706FA30AAFAE368A5E1BDE98AAE0D8514BC31F9E2AB8357155D5F0C2AB836042E0C3C69855703F4E1B4AEE78DA1706C2B4B837BE2B4BB97B6142E0DEF8AAF0DFE6315562FE98E20FC67ED1F0F6C50B03636AB837BE36ABB96155DCBE78AD37CBDF0DA29772F6C16B4DF2C28A6EB8DAD2EE58AD37CB0AAE06BDF142A29AD00DC9E8062944F2487ED0124A3A27555FF005BC4FB62C79A1DE56918B392CC7A938A5AE5FE6714BB97BE2ABB91C55DCB156C36285DCFDF15A6F9E2B4DF3F962B4EE5ED8AD37CFE78AB7CFE78AD3B9FBE2AEE7EF8AB7CFDF142E0DFE63155E1BE9C50A8A4B1006E4EC062AA8F38801446ACE76790744F65F7F7C56AD03CFDF164EE78AB5CF15A717A62AB79FCB15A773C569AE5EF8AD2DE5F4E2AD72F962AD16F7C52D72C50B791F97BE056B97BE2AB797BE0BA55A4EF8929A5A5BDFE8C0B4B4B629A5A5BE8C534B4B7D38AD2C2DFE6314AD2D8AAC2D815696F7C55616C6E954CB60B4AD2DF4E0B5585BDF02ADE58AAC2715585F14ADE5BEF8AD3B90C5696D4E29A6AA7C698AB45B6EB8365B5BCBE58DAEED1638EE96B15A757155A5B02B5CB156AA7156AB4C55A27155A4FFB58AAD2715754F8629A6AB8136B4B1ED81775BF3C6D69AA81812D1638AADAF8E2AB4B7862AB71568B62AB49AE2AB490302696927E58556D702B45BC314D2DF7AE0568B7862AB715762AB49C556E2AD1C55A27B77C16AB09FA71A55B5276C04AADE9F3C09A713DF165C96F2C504DADC5785D8B275715584938AB58ABB155A4F862AB714BB143448C8F3559925760255BC8A6D696F0C20216612534DE45406B14B44D3E78A16E296B156F145BFFD0F1E86F1CEC9E4550357BE2ABAB855783E38AAEC55706C557027E7855783E07142E0DE38AAF07C3142E0DF46155C0FD38AAE0DEF4C55786F1C55786DBDB2568A5E0F81FA31B4375F1C2ABC37D38A1706F038AAF0DE38AB60E155FCB142F0DF4E1B42E0DEF8A69706A6285C1B142E07E8C55772C2AD83F461B4AE0DEF5C1685DCB240A5706F9628560DE98FF002CFE03FAE285BCBDF0AD2EE58AB7CBDF155C1B155DCF155DCFE58AB7CB1B55FCB156F961437CBE78AD2E0D8A1556AC761BF7EDB7B9C28A54330405623BD28D37F01E031B5A51A8C16AEE47B1C6D697F2C569DCB0DA1BAFB62ADF2F9E2AE0C30AAEE5EF82D5D5C6D5BAFB636AEE5ED855DCB156F97BE056EBEF852EE5EF8A1B0DF4E2AB837D18AAA292480372760062AA8F37A40A237EF08A3C83B7B0FE271401684E5EF8A5DCBDF15772F7C556D715772F6C55AE58AB8B53B60B56B97BE36AB4B9F1C55DC81F1C296B9E0B56B960B5A5A5BDF05AD2DE58DA69A2DF462AB4B7BE29A5A5B155A5F14AD2D8AAC2D8AAD2DEF8AAC2D815696C169A585B05AAD2D8AAC2DF4E055A5B15532DF462AD13FEDE29532DEF5C569616C1690D72C6D5AA9C6CAD3AB8AD358A5D8AB5518DAB5CB02ADA9C55D8AB448C55AE407B629689AE285B5C556D714B8E0B4D355031DD0B791C764B582D34D134C0AB4927155B5A62AD72C556E2AD138AAD249C55A269812B391C55AC55693E1F7E2B4D604AD27C3155BD715762AB49FF006B155A4E2AD62AEF7EA705AADE58AAD27BE3C95656BFD9815ADB03201AAFBE292561F1C58EE5D8B201D8A5A2698AACC55D8AB44D3155A4D7156B1577BE0255696ED800B55B9256F224AB44F8E04F25849392A43B0136A1AC0C9D8AB44F862AB315762ADE28E6D6297FFFD1F1D5478E760F27CD7034C36821786C285E0F8E2ABC1A6156C362AB81C557541EB8AAF07E9C50B81C2AB8378E2ABC1C2AD86F1C50BC1C5550362ABAA0FB636B4BAA47BE1B416EBEF4C285D5F0C2AB836285FC862AB837BE2AB836155C1BDF15A5DCB1B42E0D855706FBF1452FE5BF5C55B0DFED6285F518AB75F7C55595B800C69C8FD81FC708559CABBD7738414AEA9C36ADF2C1685DCBE9C2B4BB962B4B837BE2B4DF2C286F97CF155DCBDF155C1B155C1B15554DFBD147DA63DB1436D2EDC14714EFE27E78DA80B037F99C52BB962ADF2C6D14DF2F7A636B4DF2F7C55BE5ED86D69B0DED8DA29BE78169DCABDF0DA29BE5EF8DAD37CBDF1B5A6F97B9C6D69AE4DE38DAD2EE58DAD3B9636B4DF2C6D0E0D86D5783B81D6BE18DAAA34BE9028BFDE1D9DBC3D87F1C49543F2C16AD72F6C6D345BE4315A2D72C569AE47B9C6D69C5861B5A7721E38DA69AE43C6B8095A6B9E0B5A6B962B4D72F7C534D16F7C569696C5696F3C52D73C55696C55697C55696C0AB4B62AB4B62AB4B7D382D2B0B75C16AB4B62AB0B7BFD18AADE5815696F7C55616FF00318A5696C569616C6D2B3960B4ADAE2AD54636AEA8C6D5AE5ED815AE4715754E2AD62AD57156AA3156B962AD57156ABEF8A5AAFB605A6AA71255C48C53616F2F018D216F5C169A76C305A42DE58AD344E2AB6B8AB5C8E2AB7155A5BC3156AB5C556920604B44F862AB71B55A4F862B4B7DCE2968B7D3815A26B8AB58AAD27155A4D715762AD6FD7F0C5569DF07355A4D063C95675DF05AB581205B44D3165C96938B125AC5203B164EC55613E18AB58ABB155A4F862AD6056B0AB551F2C16AB49AE202B586D5BC8DA5A269800B5B5877DF25C90EC04A69AC09762AB49FBB155B8ABB15762AEC55D82D5FFFD2F1BE760F2842E0714725D5C2B4B81231B62B830C2ABC1F7C5570270AAF06B8AB60D3155F518AB60FD3855757E8C50B831F9E155C0E2AA81BDE98A1786C55772F7C55772F1C55703E186D042EE5E230DAB608EC71B42EAD30AAE0DFED6285DCB155C0FBD7155DCB155C1ABDF0DAAEE58DA1707386D577238A29514D0726DC0E83C4E2AEE649A9EA70AB61B6EB810BB96296EBEF850BB96056F97BD30AB7CBDF1B55DCB0829706F7C92AE0DF2C1685E1BE8C5555694E4C78A8EA7FA628734BCB6028A3ECAE15A5BCB156C3FBFDF8AAEE58AB7CB156C362ADF2F962ADF2C55BE5F3C50DF3C52DF3F962ADF2F9628772F6C569BE58DAD37CB15A772C569DCBDF02D2EE5F4E15A5D5C6D8AA190C7500D243D4FF0028FEB8792694397BE04BB97BE2AEE5EF8AD35CBDF02D3B9E15A6B9605A772C569AE5855AE78A5AE78AB5CB156B97CB155A5F1568B6055BCFDF0AD35CBE78AAD2D815A2DEF8DAD2C2D82D2D72C16AB0B7D18AAD2DF4E2AB797D1812D72F7C5561618A1696F1DB14AC2DF4E2B4B0B7860B4AC2DEF8DA69696C0AD57156B15762AD5462AEAE296B9628772C52B49C50D123156AB8AB552714BBE9C169A6AA077C77435CBC31A4DB55C16B4D636B4D16C095B538ADB58AB45862AB4927156AA062AB797862AB7156890314ADE47E58AB5815696C569A3BE3696890302ADA9C55AC55AAFF00B78AB55FA7155B8ABB155BD3FAE0B55BCAB8AAC268705AB55C096B16402D27B628256E28E6EC59D3B1568903155A4D7156B15689A62AB49AE2AD62AEC04D2B5CB06E556D7080AD624AB7914AD2DE184042CC374AEC16C9D815C4818AAC26B8AB58ABB15762AEC55693E18AADC55FFFD3F1A57FDACEC1E557035C55BAE28A5FCBEFC517DEB8118AD2EE5ED5C36C5703EF5A6155C0FB6155E0E2ADD7155DD3155DCBC7155C0F861B56F9628A540D855703E18AAEE5E38A1703E07155FCB155C18E2ABABD310514B813D8E1B5A6F978E14375AF7C557570AAEE5EF8A1B076C55703EF8AAE0D8AAA29AEE4D1475C2AD99391F6EC07418DA29DCB1B55DCBDEB850DF21F2F962B4BC378E2B4B81F0C55B0C298A1BE58AB7CBDF0AAEE4702B60E15545A752761D7082AE690B53A003A2E36AD54F863696F963685C1FDF0DAD37CBDB15A772C569772F7C514DF2F7C55BE58AB7CB156C37BE2ADF2F7C55BE5F2C2AEE5815BE78ABB97CF1B5772F9E156C37BFDF8AAF0D8AAA73E0A0FED9FB3EDEF8A1479FBE296B9FBE2AEE7EF8ABB9E05773C2AD73F9E05772F9E2AD72F6C55DCBE58AADE78ABB97BE2AD72C556F3C2B4D73C16B4D16C1695BCB1055AE78156F2C0AD16F7C2AB7962AB4B60568B7D38AACE58A5696F7C55696DF738AD2CE5E07025696FF6F1B4D2D2DF4E055A5B15584FD38AB55C52D54F8E2875714B55C55D5C50D13F4E2AEAFB629A6AA715A760B5A7540F6C536D721F3C56D6927B60D9776AB8DAD3BA6369689C0AD72C556FCF156AA315689C556E2AD5462AB6A7156B1568B0F9E29A5A4938AB55C0AB6BE18AD2DDFBE297540C0AB4938AB58AB55C55A27FDBC556E2AD576AE2AD72182D5613BD7AE34AB4B636AD6056B03201D8A6E961C58F36B1480EC593AB8AAD27C3155B8ABB155A5B155B8ABB157540C04AAC26B880AD6156F224A5A3B605B5A4D7240216E02534EC096F142D269F3C52B7156B156F14358A5C4D315584D7156B15762AFF00FFD4F18034CEC1E5570F9E2AB81A6DF8E2ABF156C1A628A5C1B145AEC2B4B8353BFD38B15C1BDF0AAEAE36ABAA7C70AAEE58AAEC55B04FCF155E09C55703F41C2AB813850B81FA302AE070A1706EF8AAFE47156F97D18AAE0DF4E2ABAB8DAAEA9EC70DB1A6F9788C295C1878D31437538DAD2E53534E9E270A171704D06CA3A62AEE58AAEE5EF8AB7CB155DCB156C3781C36ADF238DA1706C36B4BF91F9E28A706F1C569706A9EB8AD2A29AD6A6807538AB8C95E9B28E830A1AE58A57D7143ABEF815BE5F2C55B047861B4B7C87CB0DA1BE5EF82D5DCBDF0DAAEE471B4BB9610ADF2F9E286F962B4EE7F2C6D69DCBE58DAB7CBDB156F9FCF0AD37CBDF05AB61BE9C6D0A81828E477FE55F1C554CC849249A93D4E369A6B9E0453B9E2B4EE786D2EE5EE71B5773F9E36AD72F9E24AD35CB1B56B97CB012AEE5F2C16AEE5EF8DAB5CB1568B138156F2F7C2AEE5EF815AE5F3C2AB796056AA7C7155BCB1568B62AB79ED8A5696C569697DB14D2C2DE182D5A2D8A69696C0AB4B1C55616FBF155A5B155BC8E29A6B7F1C569BA9C6D69C77C169A77D38DAD35F4E36B4D74DEB8DA1DC878E3BADB5CBC31A4DB5C8E0A0BBBAA71B5A6B1B4D3AA305A1AA8C52B6A7156B156AA3156B97862AD54E2AD57155BCBC3156AB8AB5815696F0C29689AE055B5A62AD127B629A5BF3C6D5D50302ADA9F962AD62AD57155A4E2AD138AB55FF006F15689A7BE0B5689DA9839AADC3C9561F0C16AD62902DA270321B2D27C31412D62A03B1480EC52B4B7862AB715762AEC556135C55AC55D4AE24AB44D36191E6AB30D2BB1255D912534D134F9E2156F5C97243582D2EC09762AB49F0C556E2AEC55D8ABB15689A6055A4D70AB58ABB15762AFF00FFD5F16D4E75D6F2ABBE9C215703E3855754F6C55703E38AAEAF862A4360D31452E0D85176BAB4EDBE0521706386D8AE0D855703E38DAAEC2ADD4E2ABC1EE3155DCBC7156C1F0C2ABB9628A5E0FBE2ADF2F1C55703DC6155DC8E286C37D18AAEA9C569706C55706F7C55BE55EB8DAB60F875C368A542D4D86FE27C70A2A9AE58AB751E38DAAEAE1B56F978E2AD86F7C50B830FF6B156F97BE2AB8362ADF2F6C55B0D5EF856978249A57E9C569717EC3A0C6D14D06F738AB7C861B452EE78AD37C878E15A6F90C569C08AED8AAFE5810DF2C55DC8615772C55BE7F3C55BE5EF8AB7CBC698DA5DCB14375C55DC878636AEE431B56F97BE1055514F527603A9C6F74AD69391AD69E03C31B42DE5EF8375772F73855DCBDCE36AEE5EF815DCBDF15772F9E2AEE5EE7156B96296B96043B91C55DC8F8E1568B7CF02ADE58AB5CBE58AB45FB62AB79E297731E38AD2D2DBE2B4B79E2968B62AB4B60B4D2DE58AD35CB02AD2DEF8AADE43155A5B156AA4E29A5A4F89E981696F2F0C55D538A68BB91F6C0B45AA9C765A6AA7C71B0B4EDFC71B5A761B5A760B5757025AA8C55AE58AD3AA715A0D54E2AD5462AD5462B4D72C55AEB8AB551E38AADE58AB5538AB58AB5C8629A5B53F2C55AC0AD12062B4B6A4FB629A6BE78ABAB8156924E2AD62AD57DF155A4FBE2AD62AD62AD569D705AB8914F9F6C554C9A62AD13514C04AADC09A757164052DE58A095B8A2ADD8B2029D8A5A2698AAD26B8AB58ABB155A4E2AB71576F8ABB61D705AAC26B8D2B5855D912534EE9815696AE1A42DC24A69BC8A80D62971DB15584D7156B15762AEC55D8AB44D302ADC2AD62AEC55D8ABB05ABFFD6F15D41F6CEBB93CAB7D31E6AD83855703F46155D5F1C6D570C55757155D8AB60D31634BB962B6D838AD375392B62BEBF462ABC11FDB8AAE07C0E155DCB156EB8AAE07156C376C55703EF8AB7CBC70A29706F7AE2ABB978E2AB837BD3156EA70A1772C55706F7C557F2E3F33D7E58AB41B155DC8FCF156F9636AB81F7C36821BE470D85A6F9788C6D57540EF8DAD375C6D0DF2C2BBB61BDFEFC50DF227FA62ABB90A53EFC55DCBDF155C18E2ADF2C2AEE43156F97BE2AB81C6D5BE58ABB963686C361B56F90C6D69B0F8AD37C8E28A706DFAE2B4BB9F862B4DF3F7C569C1CE15A6F97F9D7021DC8FCB155C0D683BF8E2AE692BB03F08E9EFEF852B7962ADD4E2877207A1DF025DC8E1435CF14BB97F9F4C55D5237C08772AE296B97F9D7156B978E15A6B99F1C0B4D73C569DC8F862B4E2F4C569672C6D34EE582D56F2C52D16F7C5696F2C55AE5815A2DEF8AADE58AADE5BE2AD7238A5AAE0B55BC878D71B56B9780C169A5BCBDF1B50D5460B4D3B90C55DC862AD72C55DCB15772C55DC8E2AD7238ABAA7156B1575462AD7218AB5C8E2AD54E2AD62AD5462AD72C55AA9C55AC55A240C09A68B786155BBE0576D8AADE5E18AD35B9C52D6C302DB5CBC3155B8ABB156B156B97D38AAD27156ABBFF001C55D8AAD2D4C0AB4B62AB09C6D5ADB229762C806AA07BE2A4AD27163CDAC5953B14BB15585BC3156B15762AD134C556935C55AC55D809A56B960E6AB4EF92015AC04AB791252D12062B6B09AE4AA90EC04A86B032762AD134E98AADC55AC55D8ABB143B14AD27B62AB715762AEC55D8ABB22AD6057FFFD7F138DF3AD7962B81A628A5D5070F256C123155C0E1B55C0D30AB60FD18AAEAF7C557038AB60E2ADE28A6F9628B5E0E15A0B81C6D485C1B0B15E0D4EF8AAE070AB7CB156F962ADD41C5577238AB7CB155C0FBE142EE58AD381C557723F3C5550100027AF6FEB855DC89EF8AB7CB1437518AB75F7C55772C55B0D8AAEE5EF8AB7CBC71B56F90C36B4DD7DF1B4537C8E3685DCA9BF739257721E34C0ADD7DEB86D5BAFB636B4DF2F7C55BE5EF8A1BE58AB7C877C55BE43156EBEF8ABB97BE2ADF23855BE5ED8ABB96055DCBDF0AD3B97BE36B4DF2C6D69DCBDF1B453B91C6D69706F7C6D5717E2388EA7ED1C36AB39636AEE471B5A772382D69DC8E36B4EE470DAD3B91C6D69AE4705A5BE58A29AE58DA5DCFE58A29AE55EF8DAD3B97BE29A68B6056B97B62AD72C55AE5EF8ABB97BE2AB4B62AD72C556F2F7C55D5C534D13F460B55BC878D7156B9780C6D5A2DEF82D905A4FD38DAD35CB02AD2DEF8AB5518ABAA31575462AEA8C55DC862AEE43156AA315A772C55DC8E2AD54E2AD62AEC55AA8C55AE58AB5538AB58ABB14B5C8605A6B97862AB6A7C715757156B90C5696D49C534D6056B90C55AA9C55AC55D8AB5518AAD27156B15762AB6B4EB82D5C48A62AA64D3156AA4ED82D56E29760480D134C536B6B5C58F36B1480EC593B15689C55675C55DD315689A62AD13E18AADC55D8ABAA064495584D7080AD6156F224A5AC0B6B4B787DF84042DC6D5D8193B15774C50B49AFCB14ADC55D8ABB15762AEAD302AC249C2AD62AEC55D8ABB012AD6455D8ABB157FFFD0F12675AF2D6B8378E2ABB15A6C31F9E1415D5AE3C957548C7DCADD70AB60FD1842AEE5E38AAEF91C55D538AAF07FDAC55BC504360D315A2BC1FA714735E09A8C2C5B2D855B0462AB81C6D5B06B855B07C3155DCBC7155D5C55C0FBE2ABB9788C557A91D49DBBE1571624E36B4DF2FA3142EAFBE2ADF2C55BA8C55BAFBE2ABB91C286F962ADF2F7C55BA9C55BE5ED8AAE0DDFB62ADF23E35C55BE5ED8AB7518DAD37CBDF0DAB7CBC0E368A77238DAEEDF2F6C2ADF21EE314375A0EB8DA69D5F7C6D0DD4E36B4EAF8E15A6F97CF15A6F97BE286F9629772F9628772F6C55BE5ED8AB7CB14AE0D415AFCB142DE5F3C55DCBDF15772F7C55DCBDF15772F7C2AEE5EF815DCBDF15772F9E2AEA8C52D72C50EE5ED8ABB96295BCF1437CB14ADE5F3C55AAFB62B4EA9C16AD57DF1B480D123E78DAD355F6C6D69AE4705AD35CBDF1B486AA31B5A6B96055A5BDF155BCB1568B7D18AB55F7C55D51E38ABAA3C715762AEC55D51E38AB551E38ABAA3C71575462AEE58AB5CB1577238AB553E38AB58ABB02B5518A69AE58569AA9C0AD75C56DAA8C6D5AE4315A6AA714B5F3C6D5C481815AE58AADC6D5D8AB55C55AAFF98C556935C55AAE2ADE2AB49A60B56B962AB4B63C956135C6D5AC0975702406AA314DAD27DF1636D62CA9D8A5D8AB44D3155A4D7156B15762AB49ED8AADC55D8ABBA60B55B5C1CD56E495D8095760B4D389A6008595272554AD60B4D378169AC52D138AADAD7156B15762AEC55D8AB44E2AB7156B15762AEC55D82D5AC8ABB157134C2156D4E1A57FFFD1F10024675AF2EBC1AE2ADEE3142EE5E38A577D38A1B0DE38AD2E041C37486C123156C1C2ABABE186D57038AB7507156C138AAE07C7155C0F86282B8B7618A90DD70A2C15D5FF006F02D37C8E1B452E0DDEB8DA17570AB75C6D5BA83855757155C0927F8E2ADF207D80E98AB81F0C55BA9C55BA8C55B07C0E2ABAB850DD71B5A6F97BE2ADF238AAEE43157721855703EF8AB7CB1437C862AEE5EF8AAEE47C7156F91C55DCB155DCBDCE2AEAFBE2ADF2FA7156F962ADD70DA29D51F2C6D69BAFBE36B4EE5EF8DAD37C8FB636B4DF2C6D2EE5ED86D8BB97B62ADF2F9E3BAB837BE156F903DFE58AB5C878E05D9BE5EF855DC8788C16AEE43C4636AEE43C4636AEE5EE31577207BE2AD5478E15D9DC8789C0AEE43DF156AA3C3157547860B48772C52EE471B416B97BE36B4D57DF1B5A6B97CF1B4D35CB02B5CBDC62AD72F7FBB156B962AD72C569A2DEF8AADE43E78AB5CB156B97BE296B143B15762AEC55D8ABB15762AEC55D8ABAA3C714B551E38AD3B9605A772C55AE47155BBF8E15B7605B6AA3C71577218AD35CBC315A6AA714B5815D518AB44F862AB6A715762AEC55A27155A4E2AD62AEC55ADF1575705AACEF8AAD269B63C95A26A2982D56E04D3ABF462901A2698A6D692714736B1501D8B2A762AD123FB3155A4D7156B15762AD138AADEB815AC2AEFF003AE0E4AD1207BE0BB55B5AE48056B1B56F224A69A3B6E702DAD2D5C3485B86D34DE454358A5D8AAD26BF2C556E2AEC55D8A1D8A5D8AAD27C3155B8ABB15762AEC55D912AD605762AD13920156E15762AFF00FFD2F0F8A1E87E83B67594F2EDD08ED4C2AD834C514B8107156EB4C55703E38A57628A6C13F3C282B8118EEADF4C55B070EEAB81F0C55B069855703F7E2AB83536EF8AB6187F6E2ABB157628A6F91C5775DC8628D9756B8569BA9F1C6D14E0D8A17022BE3855509A0A03F3C55AE58AAEAFD1855BE5EF8AB7CBDB156EA3156EBEF8AB7538AB75F6C55BE5F462ADF2F718ABAA70A29772F6C55BE58AB7538DABB91C2ADF218169706F7C569BAFBE2ADF23850EE5ED8AB7C862ADF2F7C55DCBDF156F97CB156F97B62AEE58AB7C862AEE5F3C55BE5EF8AB7CBDF15772F962ADF2DBDCE2AEE471577238ABB9628B772C29772C50EE58A5DCB05A1DCB14BB91C55AE5F2C55DCBE58AB5CBDF15772F7C55AE5F3C55DCB156B97B62AD72C55DCBDF155A5BE9C55DCB156B962AD72F7C095A48F1C2AD72F6C55AE4702BAA7156B0ADBB02BB156EB8ABAA715754E2AEA9C55D5C56DD5C56DAC56DD8DADBB1B56AA3C71577218AD35CB15A6B91F0C534EA9C55D815AA8C55AE43B62AD54FCB156AA71B5762AEC55AAE2AB7962AD62AD5462AE06B8AB47A60B56ABB7862AD16C695616F0C6D5AAFB0C16AE26B8120355A62CA9AA8C56D6938B1B2D629E1762C9D8AB44818AAD27156B15762AD134C556927156B15762AD12060B55A5ABB634AD615760B48764569A2DE18405B5B5AE1AA43B05A86B032762AD134C556F5C55AC55BC516EC55AC52D134C0AB49AE156B15762AEC55D82D5D815AC0AEC2AD1386956E15762AEC55FFD3F0DE75774F2EB8311DF0DAAE0C0F518AB7F2386D5BA91D71452EA838AB7B8C55772F1C569BDB152D834C28A5C1877DB02AEF961B5754E2ABAB920AD838AAE07156C1C557027156C1F1C55757157628A6EB8AEED838516BC1A7CCF4C568381A61B4537CBC71B4361B155C1BFCCE2ADD7E78AB60E36ADD46155C0D31577238AB60F8E2ADD462AB81A74C55DC8E2ADF2F6C55D518AB751D8E156EA702B7CBDB0A29DCBDB14D37C86286C37BE2AD86FA7156F91C55DCB15772F6C2B4DF2F7A605772F7C2B4DF2F7C55BE5F2C50DF238ABB96056F9615772AE2AEE431577218ABB97CF15772F9E2AEE5F3C55DCBE78ABB97CF1577218ABB90C55DCB156B97B62AEE5ED8ABB91C55AE5EF8A5AE5EF8AD35CBDF156B90C569DCB02B5C8E2AD72F7C55AE43E78AB5CB14BB9628A6AA7C7025AEB86D5D8DAB7BF8E2AEA9C569DC8F862B4EE47C315A7723E18AD3B91F0C569DC8F862B4EE47C315A754E05A754E156B7F1C6D5D4C6D5D82D5DB6156B90C0AEE5ED8AB5C8E2AD62AEC55D8AB551E38AB89F7C55AE58AADA9C55D8ABB15689A75C55AE4305AB44E2AB0B6FD31D95AE58DAB5D4E455D5C595344E29A01AAE28E2689ED8A39ADC5203B164EC55AAD3155B5FA3156B15762AD138AADA938AB58ABB7C55A2705AB44E34AB70ABB05AB7912968903DF1A5B5A4D7240216E02534EC09762AEE9D7155A4D71452DC52EC55D8A1D8A5D810B4B78614ADC55D8ABB15762AEC892AD605762AEAE10AB6B9256B15762AEC55D82D5FFD4F0B8639D5BCCD2F041C20316F05AB60E155C0FFB58AAEDBBE36AD83EF8557546286F156C378E295F5AE28A70C505772F1C3C95BA83ED8EEABAB4C42BABEF4C2AB81C55BE5F2C2AD83E35C55B1ED8AAEA9C55BE43155C08EBDB1575714537538AD37CB15B2D823146CEA8C5697570DA29BE4708DD0BAB8DAB7855D5F7DF02B7CB1B55D5070AB75F7C55BAD3BE2ADF2C55DCBDB15772C55BA8C55BAFBE2AEE5EF8AB7538AB75F6C55D51E18DABAA3C70AB7C878E2ADF2F71815BA9C50EE47C30AD3B97B6296F9628A77218A776F90F1C50EE5EF8DABB97BE2AEE47C7156F91F1C55AE47C71B57723E38DAB7C8F8E36AD723E38DABB91F1C6D5BE5EF8DAB5CBDF15772F7C55D5F7C55AE4314BB962BBB5CBDB1453B91F0C569AA9C52EE5EE315772F7C0AB797CF0ABAA302B5538AB553E38ABAA71577D38DABAA7C7156EA7157723E18ABB97B61A56F960577218ABB90C55DC862AEE5F3C55DCB1A56B97B6157723815D538AB553E38ABBE9C6D5D8ABB15762AEC55AAE2AD72C55AE58AB5D715762AEC55D8AAD26982D5AE58EEAB49AFCF1A5713B571B5595C16AEDBC7E8C096B14F0B55FF6B14DB5CBC314712DC5145D8B2A7629762AD12062AB4938AB58ABB15762AB791C55AC55AC55D80956B97860DCAACC900AEC04D2BB05A69D5F1C0BC9696F0C20216E12695D914D3B14BB15689F0C556D6B8AB58ABB156F145358A5C4D315584D7156B15762AEC55D80ABB02B5815D8AB44E10156E495D8ABB15762AEC8AB5815FFD5F0AE756F34EC55706FA71452F041E98A1BC286C1C79AAE07156EB8DAAE0D8514BAA314B78AAEAED5C50EE58AD3608C54AE0698A17541F6C2AD8F638DABAA7156C1F9E1B55D5C6D5B070AB60E2ADD46DBF4C55B04E2ADD7156C118AB75C55BC514DD4E2B4DF2F6C5777721850DD4605D9BC569B048C368A5DCB0DA1DC81C16ABB953BE15773FF00338AB75F7FC7156EB8DAB7518DABB978570AB78ABB97BE2ADD7156F962AEE43156EA31575462ADD715757DF157027C7156F91C55DC8E2AEA9EF8AB7CABDB156B97B62AEE5ED86D5BE5ED8DABAA3C302BAA3C3157547862AEA8F0C55D51E18ABABED8ABB97B61B5772F6C0AD57DB1577238ABB91C55D53E38AB55F7C55D5C55AA8F1C55AE4315772C55D5F0C55AAF8E2AEAEF8AAE071575478E2ADE2AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AD5478E2AEA8F1C55AAE2AD57C3F1C55D538AB58ABB15762AD1A8C55AAFCCE2ADD702AD3BE2AB4ED8D2B5CB1E4AD13514C16AD62AD60654EDF14D06AB8ADB5CB1636B6BBE2901D8A69D8A5D8AAD271568927156B1577B62AD134C55A27C3156B05AB5855D82D5A247CF07355B5C34AD61B56F224A5AC0B6D72F0C3485A708D95D8094D35812EC55A2698AAD249C55AC55D8ABB15762AEC55693E18D2ADC55D8ABB15762AEC04AB5915762AEC34AB49C34AD615762AEC55D80956B22AEC55D8ABFFD6F0AE756F34EC55D8ABB142E0C71B5A5E0838A1BC6D0EAE1B55D5C6957636ADD7DF0AB7C8E2AB8107DB14378ADB61A98AD2FA838A29DBF8D31490BB91F9E286EA3E587756FE471B5754E2ABABDBEFC36ADD6B8DABABEF855772F0A60B57547BE156C62ADEFE38AB7C8F7C55BE58AB75C55BC569D5C514DD4E2B45D538AEEDF2F1C516DF2186D7675462B41BAE36BC2B836368A7571B5A5C0E3B21DCCE2AD72C36ABF97D18AB5CBDEB8AB7538DAB7CA98DAB75C6D5D5AF4C2AEAE2ADD7156EA7C7157723E38ABB96DFC7156B97BE2AEE5EF8AB7538AB7CB156AB8ABAB8ABAB8ABAB8ABAB8ABAB8ABB97BE2AD72F7C55BE5F2C55DCB15754F8E2AD57156AB8ABAB4C16AD7218DAB81AE36ADE36ADE15762AD571575462AEC55BC55D53E38AB7538ABAA7156AB8AB7C8E2AEE4715757156AB8AB75C55D538AB55C55D8AB551E38ABAB8ABAB8ABB156F15760B5595DF1B56C93DB12AB4B634AD72A63C95A276AE0B55B538DABAB5D8E056B1654EC534D5462B602DA9C516D7EBC5156EC5953B14BB15774C55696F018AB44D7156B15762AD12062AB49AE2AD62AEC55D80956B953DF06E556935C202B58956F05ABB0295A5BC3080AB6A4E1AA56B05A69BC096B15762AB4B7862AB715762AEC55D8A1D8A5A2698AAD26B8AB58ABB15762AEC55D91B56B15762AE2698D2ADC92B58ABB15762AEC8DAB5815D8ABB15762AFF00FFD7F0AE756F34EC55D8ABB15762AEC5570623145361877C514BB14375C36ADD71A55D5C55BC6D5BC2AB81F1C55BC50DD48C55706F1C569BA8C54B78DA1BAE216977218ABAA0F7C36ADEE31D95BAE2AEAE1B55D5F7C7657636ADD70AB7CBC7156EA315757156EA7156EB8AB75C55BA8C55D5C569BC514EAFBE2BC2EA9F1C569BE4715DD706C5165DC87BE2AEE5857675478E05A0DF5F7C569D851C2B81A636B4D57DFF001C6D69BA9C368772C55B2D8ABB97B605772A7CB156F97BE36AD72F7AE15772C55BE40F8E2AEE5EE715772F9E2ADD4F8E2AEA9F1C55D523156B97CF15772F9E2AD7218ABB962ADF2F7C0AEE5EF8DAB5CB156B962AE2DE18ABB9636AEE471B57547BE1B55E0D77C695C71A56B06EABF90C6D5DC861B56EA31B56B90C2ADFBD71576F8AB78AB5BE2AEDF1576F8ABB7C55BC55D8AB54F13F762AD5698AB75182D5AE431B577218DAB89A8C5567D38ABB1A575698ECAB79636AEE582D5AEB8DAB5812EC534EDF14D068B7DF8ADB553851C4B702EE5D8AF0BB1654EC55D8AB5518AADE47156B15762AEC55A2462AB6A7E58AB58ABB15760B56891F33839AADA9C34AD6156F236AEC0A0AD269EF8A6D6924E4B921D82D34EC0A1AC52EC55A2DE18A296E296B15762ADE286B14B89A62AB49C556E2AEC55D8ABB15760B576056B02BB0AB44E10156E15762AEC55D8DABB22AD605762AEC55D8ABB157FFD0F0AE756F34EC55D8ABB15762AEC55D8ABB156C1231452E0DE38A297541C2421BC16AEAE1B55D5C6957571B576155C18E28A6F903D76C55762B6D83DEB8ABB978E295DC86282EC569B04F8E282DF23DF0AB7C87CB1DD5BDBB1C6D5BC7656EA70ABB91C2ADD705AB75C6C2B753855BE4715757DB05ABABF3C2ADD7156F156EA7156F91C55D5C55DCB156EA3156EA3C7157628A762B4EC569BAE2B4EA9F1C568BAA715DDBAE146EEE5E18AEEEE5816DC1B15B0DF2C56C3B957AE2BB3AA315D9D518AECEE58AECEE58AECDF338AECEE6715D9AE78AECEE58AECEA8C5766EA315D9AE4315D9DCB15D9DCB15B0D72F6C56C3B962B6DD4629B6F15A7628E17628A6C1A6286F91C36AEE471B5706AED86D57636AD5462ADFD38D2B61BB634ADF238295AE5F2C34ADF2C695AE5F2C77577238EEADF238D2BB91C695DC8E34AEE58D2ADAE34AD54634ADE36AD134C6D5AE4705AB553E38DABB91C0AEAD7AE29A6B0A785D813C2D5462BB077218AF12DAE28B6AB851CDD813C2EC534EC52EC55D8AADE5E18AB5538AB58ABB15689A7CB156B97862AD127156B15762AD53BD705AB5CB06E55A26B84056B12ADE0B570C04A96890315B5849392015AC495764534EC52EC55A27C3155A4D7156B15762AEC50EC52EC55A27052ADC2AD62AEC55D8ABB015760B56B02BB157569920156E156B15762AEC55D80956B22AEC55D8ABB15762AEC55D8ABFFFD1F0AE756F34EC55D8ABB15762AEC55D8ABB15762AEC55D8AAE0698A086F90C514B81071A56EB86D0DD71D95757156EB8DAB630AB7C8FDD8AB7CB143751E38AB78ADBAA7155DCBDB157546295D8DA1DF49C3686F91C0B4DF2F118AB75070EEADFCB1B576F8D85754E156EBE2306EADD461B2AEA8C6D5D5C6D576F855D5C55BAE2AEE43156EB8ABAA315757156EBF3C55BA9F1C55DCA98ABB91C55BE58ABB962AEE58AB7C862AEAE2AEA8F1C55DB78FE38ABAA315757156EB8ABAB8AB58A3675462B4DE2BB3AB8A5D5C55AAE2ADD478E2AD62ADE2AEC55D8A29D86D14DF4C0877238ADB7CBDB165C4B81AE2B6EA9F1C569D8A385D8AD3B1453B0ABB021D8AB786D5D8DAB58DABB1B57636AEC6D5BA7B8C6D5DF4E36AD605761A57604D3B15A7629E17629E16EA70DAD0689C0B616F2C51C4D7238ADB89AE28E6D62B4EC5970BB14D3B15762AD571568B0F9E2AD54E2AD62AEC55D8AB5518AAD271576056B0ABB1E4AD5705AB55C695AAE34AD61B56F236AEC0A0B4580F7C6956135C952B58DA69D9150EC52EC55A2698A16D4E296B15762AEC55D8ABB15689A62AB4927156B15762AEC55D8ABB236AD605762AEC215A27C30D2ADC2AEC55D8ABB05ABB05AB5815D8ABB15762AEC55D8ABB15762AFF00FFD2F0AE756F34EC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55786F1C5042E041C690DE3687570DAAEAD31A57571A56EB8DAB78DAB609C2ADF238AD37C86286F63D3156F15754E296F962ADF2F6C514DD462B4EC36B4DD4F8E0521BE47143B91F0C34ADF21E18EEADD478E3655BF91C6D5DBE3B2B7538ECAD570AB7CB1DD5D5C6D5BE582D5BAFCB0DABABEF8DABB0AB7538AB7C8E2AD72F962ADF238AB55C6D5BAFB60D95BE430AB55F7FC31B56EBEFF00862AEE4315757156EA3C7156AB8ABAB8ABAB8ABAB8ABAB8AB78ABB15762AEC55706C55762AEC55D8ABB157628A7628E17628A762ADD4E2B6DF2C53C4DF2C5789D518A6DBA8F1C56C35B62BB378AECEC569D8AD3B15A762B4EC569D8AECEC57675478E2B61AE4315B77218AF135CB147135C8E28B2EA9C56DAAF8E2B45BAED4C53C2D62BC2EC534EC52EC55D8AB55C55AE58AB5CB156B15762AEC55D8AADE58AB5538DAB5815D855D8095689A60BB568B570D2ADC34AEC04ABB05A69BC082B4B01EF8D2DAD26B92015AC495764534EC52EC55C4D315584938AB58ABB156F145BB15A6B14BAB4C0AB49C2AB715762AEC55D8ABB05AB5815D815D8AB44E4A95AC2AD62AEC55D8ABB012AD6455D8ABB15762AEC55D8ABB15762AEC55D8ABFFD3F0AE756F34EC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB156EA7C71B410BEBB54E28A7020E34B4BB1DD0EC36AEAE36ABAB8D2B75C55BAE36AEC6D5BA9F1C2ADF238AB7CBDB1453B90C55BDBC715B762ADD4E2ADF238AD37CBDB15A7546296EA3C71416F6C6D0DD4E2AEA9F1C36B4EE4702D37CBDB15A7721E1855BA8C55D51E38EEAEDBC71B56F1B0AEDFC71D95DBE1B57571B575702B75F6C2AEAE3BAB7CB1DD5D518D956B90F0C7756EB8DAB751E38AB55C52EA8C6D0DD478E3B2BAB8ECAEC34AEC55BC55B07155DD715762AEC55D8AB7538AB7CB156EA3C7156F15762AEC55D8ABB15A7628A7628E1754E2B4EC514EC569D8AD3B0DAD3B15A762B4EC6D69D8AD3B02D3B15A2EC569D8A785D8AF0BB14D3B14D3B15762AEC55D8AB55C55AE5E18AB55C55ADF15762AEC55D8ABAB4C556D7156AB8ABB02B5855D8ABB05AADE58295AA9C34AD615760B4B782D69AC0B6D16F0C3485B5270F256B1B4D379150D629762AE240C556124E2AD62AEC55D8ABB15762AE3B62AB49F0C6956E2AEC55D8ABB157636AEC892AD605762AEC34AB49AE1015AC2AEC55D8ABB1B576455AC0AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABFFFD4F0AE756F34EC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB156C6282E26B8A86B14B7538A29BE470F35A5C0F8E3482DFD38290EC55BC6D5BA9C36AEAE34ADD71A56EB8EEADE36AEC36ADE2ADF238A29DCBDB15A6F90C569BA8F1C55BC56DDBF8E2B6EDF156EA714BB962ADF218AB7C8628A7721E38A29BA8C6D69AC369A7605A6EA7C715A754F8E2B4DD4E2877238AB7C8F86157723E18D2BB97B62AEE43C3157721E18D2B7C863BABABE18A5D8DAB786D0D6156EB4C14ABAA3BE0DD5BEB86D5D855D8ABB155C0D31B56C1AFCF156F15762AEC55D8AB7538ABB91C55BE58AB7C862ADE2AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55AE58ABB90C55AE5E18AB5538AB58ABB15762AEC55D8AB5503156B962AD549C6D5AC55D8295D855D82D5AAFCB059568B1C34AB71A57615760B486F22A43471402B4B7861A5B6AA4E1A015D80957604D358A5D8AB44818AB449C556E2AEC55D8ABB15762AEAE0B5689F0C2AB7156B15762AEC55D8ABB05AB5915762AEC2AD13E186956E15762AEC55D8ABB012AD6455D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55FFD5F0AE756F34EC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB156C9F0C500358A5D8ABB15762ADE490EC55B071410BC1A8C050EC8ABB156F0DABB1B57570DAB75F7C55D5C695BAE34ADD71DD5BAE2AEC6D5D5C6D5BA9F1C2ADF238ABB91C514EE5ED8AD37CBDB15A6EA31575478E2ADE2B6EC56DDBE2B6EA9F1C52DD4E2877238ABB91C569DCBDB15A6F97B62ADE2B6EC52EC55BC20A29AE9879A1786F1C042B78136DE10569AC36876156EB808B55C0F8E0DC2B7F8E1B57615762AEC55D8AAEE58AB7518ABAB5C55BC55D8ABB15762AEC55BA9F1C55D538AB7C8E2AD5715757DB156C378E2ADF218ABB90C55DC862AEE43156B97862AEE5ED8AB55C55D5F618ABAB8AB58ABB15762AEC55D8ABB15762AD5462AD72F0C55AA9C55DD7BE0B56B0ABB15762AEC16AD5460B2AD571A56AB8695AC2AEC169A6F05A1D8136B790F9E1A42DA93ED86A95AC6D2DE0B5A6B025D8ABB15689A62AB6A7156B15762AEC55D8ABB15762AD72C556F5C55AC55D8ABB15762AEC16AD6057605762AE26986956E495AC55D8ABB15760B5760B56B02BB15762AEC55D8ABB15762AEC55D8ABB15762AEC215696F0C952696E3497FFFD6F0AE756F34EC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8AB78AB58ABB15762AEC55D8AB7842BB0A1D8AB60EFF00AF121057E450EC0AEC55D8ABB15762AEC55D855BC6D5D8DABAB86D5BA9C2AEA9C14ADD71A56C1C55BC16AEC36AEC6D5D855D8AB78ABAA7156C1F1C557570229D852EC55D8ABB156C1A62ABC107E78ABB15762AEC514DE4814381231A55C08C895B5D8A79B586D5BA61B435855D5C14ABF97B60A2AEA8C6D5BA53BE1B576157571576215D8AB609C55BE58AB7C862AEAE2ADE2AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55AAE2AD72C55D53E18DABAA71B55B8ABB1575460A57615762AEA537AE0B56ABE183756B91C6956E1A57615760B5760B4D378169AA8F1C56D6F2F0C2021AA938695D82D21D8169AC52EC55D8AB44F862AB49AE2AD62AEC55D8A1D8A5D8ABB15689C556935C55AC55D8ABB15762AEC04ABB05AB5815D8ABAB86956D70D2B5855D8ABB15762AEC8DAB5815D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AE247D384055849392654D62AEC55FFFD7F0AE756F34EC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55BC9043B15762AB94F6C4A0AEC821D8ABB15762AEC55D8ABB15762AEC55D8AB792055D855D8ABB155E0D7010AEC8ABB156F082AEC92BB15762AEC55B0698AAFC55D8AB78A1AC52EC55D8AAE0DE38A297629762AEC55BC36821AC36B4D8246242177218295BC09B6F1B5A6B256B4EC28762ADD4E0A56F960A56EA31B56FAE1B576157634AEC55D8ABB02B78DABAA70ABAA71577238ABAB8AB75C55D5F6C55BAFB62AEAFB62AEAFB62AEAFB62AD5715757156AB8ABAB8AB55C55BFA702BB156B0ABB157634AEC695DBE36AEC16AD5478E3BAB55F9E0A56AA7C70D2B5855D8ABB05A69BC04AD3B02B58ADB5CBC061A42DA938695AC6D34DE0B45358193B15762AEA8C55A2DE18A296E296B15762AEC55D8ABB15762AD57155B5C55AC55D8ABB15762AEC16AEC0AD605762AEC2AD57C3080AB70ABB15762AEC55D82D5AC0AEC0AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB0AAD27C30D2405B852EC55D8ABB157FFD0F0AE756F34EC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8AB79243B156F15A5F91218BB02BB15762AEC55D8ABB15762AEC55D8ABB0AB784157615762AEC557035C890ADE05762ADE1B576495D8ABB15762AD834C557035C0ADE15762ADE28B6B14BB156C1A62AB830C55BC55D8ABB15761B45374C3687571A56C360A4DAED8E05B6F15A6B0DAD374C3686B0DABB15760A56C1A634ABB960A2AEAFBE3655D4AF7FBB0F12B74C6D5D86D5D8ABB15762AD57E9C55BAE2AEC14AEC695D8D2BB1A57634AEAE34AD57E8C2ADE2AEC55D8ABB1B575305AB5D31B575460B5757C31DD5A2D8D2B5538695AC34AEC55D82D5BA63C4AD60B4D378169AA8F1C56DA2D8696D6D4E1A4358F25760B4D378169AC52EC55D8AB448ED8AB5538AADC55D8ABB15762ADE28A6B14BB155A4F862AD135C55AC55D8ABB15762AEC16AEC0AD605762AEC2AD57C30D2B5855AC55D8ABB15760255D82D5AC0AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15689A61012B6A72549A6B15762AEC55D8094B5815FFD1F0C98DC6FC6A3C46E3F0CEB29E66D65302DB58A5D8ABB15762AEC55D8ABB15762AEC55D8ABB156FA62AD62AEC55D8ABB15762AEC55D8ABB15762ADE1055D8DABB1B57034C6D0BB97B60453B97B62B4EE5ED8AD3B97B62B4EE5ED8AD3B97B62B4EE5ED8AD3B97B62B4EE5ED8AD3B97B62B4EE5ED8AD3B9FB61B5A773F6C6D69DCFDB1B5A773F6C6D6977A9ED8169DEA7B62B4EF53DB15A77A9ED8AD3BD5F6C36B4EF57DB1B5A77ABED8DAD3BD5F6C6D69BF57DBF1C6D69BF5BDB1B5E177ADFE4E36BC2DFAFF00E4FE38DA385DEBFF0093F8E369E16BD6FF0027F1C6D69DEB7F93F8E36B4EF5BFC9FC71B5A6FD7FF27F1C6D6977D63FC8FC71B5E177D63FC8FC71B5E177D63FC8FC71B5E177D63FC8FC71B5E177D63FC8FC71B5E177D63FC8FC7FB30F12385DF58FF23F1C789785B1747F93F1C16BC2DFD6BFC8FC71B5E177D6BFE2BFC7FB31B4F0BBEB5FE47E38DAF0BBEB5FE47E38789785DF5AFF0023F1FECC7891C2EFAD7FC57F8E0B5E177D6BFE2BFC71B5E177D6BFE2BFC71B5E177D687F27E38789785DF5AFF23F1C16BC2EFADFF91F8FF6636BC2DFD73FE2BFC71B5E177D73FE2BFC7FB31B5E177D73FE2BFC7FB30F12F0BBEB7FE47E3FD98F12F0BBEB7FF15FE3FD98F12F0BBEB9FF0015FE3FD98F12F0BBEB9FF15FE3FD98F12F0BBEB9FF0015FE3FD98F12F0BBEB9FF15FE3FD98F12F0BBEB7FF0015FE3FD98F12F0BBEB7FF15FE3FD98F12F0BBEB7FE47E3FD98F12F0BBEB9FF0015FE3FD98F12F0BBEB7FE47E3FD982D785DF5BFF0023F1C6D785DF5BFF0023F1FECC6D785AFAD7F91F8E1B4F0BBEB43F93F1C7891C2EFAD7FC57F8E0B5E177D6BFE2BFC71B5E177D6BFE2BFC71B5E177D6BFE2BFC71B5E177D6BFC8FC7FB30F12F0BBEB5FE47E382D3C2EFAD7F91F8FF006636BC2EFAD7FC57F8FF006636BC2D7D6BFC8FC71B470B5F59FF0027F1C6D785DF58FF0023F1C36BC2D7D63FC8FC71E25E16FEB1FE4FE382D3C2EFAC7F91F8E36BC2EFAC7F91F8E36BC2EFAC7F91F8E36BC2EFAC7F91F8E36BC2D1B8FF0023F1C6D69AF5FF00C9FC71B5A6BD6FF27F1C6D69DEB7F93F8E36B4EF5BFC9FC71B5A6FD6FF0027F1C6D785DEBFF93F8E368E177AFF00E4FE38DAF0B5EB7F938DA785DEB7B636BC2D7ABED8DAD35EAFB636B4EF57DB1B5A77ABED8DAD3BD5F6C6D69DEAFB636B4EF53DB05AD3BD4F6C569DEA7B62B4EF53DB15A77A9ED8AD35CFDB0DAD35CFDB1B5A773F6C6D69DCFDB1B5A773F6C6D69DCFDB05AD3B97B62B4EE5ED8AD3B97B62B4EE5ED8AD3B97B62B4EE5ED8AD3B97B62B4EE5ED8AD3B97B62B4EE5ED8AD3B97B62B4EE5ED8AD3B97B62B4EE5ED8AD2DC369763697636AEC6D5AC0AEC55D8ABFFD2F0B0247434CEADE66977A8DDCF2F9EF8DAD3AAA7AAD0FB1FEB8AACC52EC55D8ABB15762AEC55D8ABB15762ADE2AD62AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB15762AEC55D8ABB157FFFD9, 5, N'#', N'N2', 0x, 0)
END
GO


