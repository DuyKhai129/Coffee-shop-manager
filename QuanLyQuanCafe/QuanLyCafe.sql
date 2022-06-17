create database QuanLyCafe
go
use QuanLyCafe
go

create table TableFood
(
	id int identity primary key,
	name nvarchar(100) not null default N'chưa có tên',
	status nvarchar(100) not null default N'Trống'
)
go
create table Account
(
	UserName nvarchar(100) primary key ,
	DisplayName nvarchar(100) not null default N'KAKA',
	PassWord nvarchar(1000) not null default 0,-- để khi mã hóa mật khẩu
	Type int not null default 0 --1:admin && 0:staff
)
go
create table FoodCategory
(
	id int identity primary key,
	name nvarchar(100) not null default N'chưa có tên'
)
go
create table Food
(
	id int identity primary key,
	name nvarchar(100) not null default N'chưa có tên',
	idCategory int not null,
	price float not null default 0,
	foreign key (idCategory) references FoodCategory(id)
)
go
create table Bill
(
	id int identity primary key,
	DateCheckIn date not null default getdate(),
	DateCheckOut date ,
	idTable int not null,
	status int not null default 0 ,-- 1:đã thanh toán && 0:chưa thanh toán
	foreign key (idTable) references TableFood(id)
)
go
create table billInfo
(
	id int identity primary key,
	idBill int not null,
	idFood int not null,
	count int not null  default 0,
	foreign key (idBill) references Bill(id),
	foreign key (idFood) references Food(id)
)
go
-- nhập dữ liệu cho bảng Account
insert into account values
	(
		N'ZeRo',
		N'Adim',
		N'1234567',
		1
	)
insert into account values
	(
		N'CNTTK17',
		N'Nhân viên',
		N'123456',
		0
	)
	go


--parapasster cho chức năng đăng nhập
create proc usb_login
@username nvarchar(50),@password nvarchar(1000)
as 
begin
	select *from account where username = @username and password = @password
end
go
-- nhap du liệu bàn hàm cast ép kiểu về char
declare @i int = 1
while @i <= 25
begin
	insert TableFood (name) values (N' Bàn ' + cast(@i as nvarchar(100)))
	set @i = @i +1
end
select *from TableFood
go
-- lấy ds bàn ăn
create proc usb_GetTableList
as select *from TableFood 
go
exec usb_GetTableList
go
--thêm category
 insert into FoodCategory values
 (N'Đồ ăn'),
 (N'Đồ uống')
 --thêm sản phẩm
 insert into food values
 --đồ ăn
     (N'Bánh flan',1,15000),
	 (N'Bánh Tiramisu',1,30000),
	 (N'Cheesecake',1,45000),
	 (N'Bánh macaron',1,20000),
	 (N' Mousse cake ',1,15000),
	 (N'Bánh muffin',1,15000),
	 (N'Bánh cupcake ',1,15000),
	 (N'Chocolate tart',1,50000),
	 (N'Bánh tráng nướng',1,30000),
	 (N'Socola',1,10000),
	 (N'Pudding ',1,35000),
	 (N'Kem ',1,45000),
--đồ uống
	 (N'Cafe đen nóng hoặc đá',2,15000),
	 (N'Cafe sữa nóng hoặc đá',2,20000),
	 (N'Cafe đã xay pha kem',2,30000),
	 (N'Cafe xay Chocolate',2,35000),
	 (N'Cafe xay hương chuối ',2,35000),
	 (N'Cafe xay và Orio ',2,35000)
--thêm món vào bill
create proc USB_insearchBill
@idtable int
as 
begin
	insert bill values
	(getdate(),null,@idtable,0)
end
go
--thêm vào billinfor
create proc USB_BillInfor-- alter thay đổi
@idbill int , @idfood int , @count int
as 
begin
	declare @check int 
	declare @fc int = 1

	select @check = id , @fc = b.count 
	from billinfo b 
	where idbill = @idbill and idfood = @idfood
	if(@check >0)
begin
	declare @nc int = @fc + @count
	if(@nc > 0)
	UPDATE billinfo set count = @fc + @count
	where idfood = @idfood
	else
	delete billinfo 
	where idbill = @idbill and idfood = @idfood
end
else
begin
	insert billinfo values
	(@idbill,@idfood,@count)
end
end
go
--kiểm tra cho bảng billinfor
create trigger UTG_updateinfo -- alter là app lại thông tin cho sql,create là app thông tin
on billinfo for insert,update
as
begin
	declare @idbill int

	select @idbill = idbill from inserted

	declare @idtable int

	select @idtable = idtable from bill where id = @idbill and status = 0

	declare @count int
	
	select @count = count(*) from billinfo where idbill = @idbill
	if(@count >0)
		update tablefood set status = N'Có người' where id = @idtable
	else
		update tablefood set status = N'Trống' where id = @idtable
end
go
--kiểm tra cho bảng bill
create trigger UTG_updatebill-- alter là app lại thông tin cho sql,create là app thông tin
on Bill for update
as
begin
	declare @idbill int

	select @idbill = id from  inserted

	declare @idtable int

	select @idtable = idtable from Bill where id = @idbill 
	
	declare @count int = 0

	select @count = count(*) from Bill where idtable = @idtable and status = 0

	if(@count = 0)
	update TableFood set status = N'Trống' where id = @idtable
	end
go
--chuyển bàn
create proc UAD_switchtable
@idtb1 int,@idtb2 int
as begin
		declare @idfirstbill int

		declare  @idseconrdbill int

		declare @isfirsttablemty int = 1
		declare @isseconrdtablemty int = 1

		select @idseconrdbill = id from bill where idtable = @idtb2 and status = 0

		select @idfirstbill = id from bill where idtable = @idtb1 and status = 0


		-- @iddt
		if(@idfirstbill is null)
	begin
		insert bill values
			(getdate(),null,@idtb1,0)
		select @idfirstbill = max(id) from bill where idtable = @idtb1 and status = 0

	end

		select @isfirsttablemty = count(*) from billinfo where idbill = @idfirstbill

	--@idg
			if(@idseconrdbill is null)-- so sánh vs null phải dung is
	begin
		insert bill values
			(getdate(),null,@idtb2,0)
		select @idseconrdbill = max(id) from bill where idtable = @idtb2 and status = 0
		
	end
		select @isseconrdtablemty = COUNT(*) from billinfo where idbill = @idseconrdbill

		select id into idbillinfortable from billinfo where idbill = @idseconrdbill

		update billinfo set idbill = @idseconrdbill where idbill = @idfirstbill

		update billinfo set idbill = @idfirstbill where id in (select * from idbillinfortable)

		drop table idbillinfortable
		if(@isfirsttablemty = 0)
			update tablefood set status = N'Trống' where id = @idtb2
		if(@isseconrdtablemty = 0)
			update tablefood set status = N'Trống' where id = @idtb1
end
go
-- doanh thu
create proc USA_revenue 
@checkin date, @checkout date
as
begin
	select tf.name as [Tên bàn],datecheckin as [Ngày vào],datecheckout as [Ngày thanh toán],bf.count*f.price as [Tổng Tiền]
	from bill b, tablefood tf,billinfo bf,food f
	where datecheckin >= @checkin and datecheckout <= @checkout and b.status = 1
	and tf.id = b.idtable and bf.idbill = b.id and bf.idfood =  f.id
end 
go
-- thông tin cá nhân
create proc USD_UpdateAccountIdentityCard
@username nvarchar(50) , @displayname nvarchar(50), @password nvarchar(1000),@newpassword nvarchar(1000)
as
begin
	declare @ispass int = 0
	select @ispass = count(*) from Account where UserName = @username and PassWord = @password
	if(@ispass = 1)
	begin
	if(@newpassword = NULL or @newpassword = '')
	begin 
	update Account set DisplayName = @displayname  where UserName = @username 
	end
	else
	update Account set DisplayName = @displayname , PassWord = @newpassword where UserName = @username 
end 
END
go
--triger xóa billinfor khi chứa idfood
create trigger USD_deletebillinfo
on billinfo for delete
as begin
		declare @idbillinfo int
		declare @idbill int
		select @idbillinfo =  id, @idbill = 	deleted.idbill from deleted
		declare @idtable int
		select @idtable = idtable from bill where id = @idbill
		declare @count int = 0
		select @count = count(*) from billinfo bf,bill  b where b.id = bf.idbill and b.id = @idbill and b.status = 0
		if(@count = 0)
			update tablefood set status = N'Trống' where id = @idtable
	end
go
-- tìm kiếm
create FUNCTION [dbo].[GetUnsignString](@strInput NVARCHAR(4000)) 
RETURNS NVARCHAR(4000)
AS
BEGIN     
    IF @strInput IS NULL RETURN @strInput
    IF @strInput = '' RETURN @strInput
    DECLARE @RT NVARCHAR(4000)
    DECLARE @SIGN_CHARS NCHAR(136)
    DECLARE @UNSIGN_CHARS NCHAR (136)

    SET @SIGN_CHARS       = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệếìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵýĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ'+NCHAR(272)+ NCHAR(208)
    SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeeeiiiiiooooooooooooooouuuuuuuuuuyyyyyAADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD'

    DECLARE @COUNTER int
    DECLARE @COUNTER1 int
    SET @COUNTER = 1
 
    WHILE (@COUNTER <=LEN(@strInput))
    BEGIN   
      SET @COUNTER1 = 1
      --Tim trong chuoi mau
       WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1)
       BEGIN
     IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) )
     BEGIN           
          IF @COUNTER=1
              SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1)                   
          ELSE
              SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER)    
              BREAK         
               END
             SET @COUNTER1 = @COUNTER1 +1
       END
      --Tim tiep
       SET @COUNTER = @COUNTER +1
    END
    RETURN @strInput
END
go
--  lấy billinfor
create proc getBillinfor
as
Select name,count,price,price*count as [into_money]
from billInfo bf
INNER JOIN Food f on bf.idFood = f.id
INNER JOIN Bill b on bf.idBill = b.id
where  status = 1 
exec getBillinfor

