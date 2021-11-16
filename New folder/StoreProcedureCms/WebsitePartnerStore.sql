-------------- Insert -------------------------
create PROCEDURE [dbo].[website_partner_create]
(@partner_id uniqueidentifier , @partner_logo varchar(200) , @partner_link varchar(200) , @partner_name varchar(150) , @sort_order int , @is_show bit , @created_by_user_id uniqueidentifier ,
 @OUT_ERR_CD  INT OUTPUT  ,        
 @OUT_ERR_MSG NVARCHAR(MAX) OUTPUT 
)
AS
    BEGIN
		SET XACT_ABORT ON;
		SET NOCOUNT ON;
		SET @OUT_ERR_CD  = 0;
		SET @OUT_ERR_MSG = NULL;
        
        DECLARE @active_flag INT; 
		BEGIN TRY	
		
        SELECT  @active_flag=ISNULL(w.active_flag ,-1)
		FROM dbo.website_partner w
		WHERE w.partner_id = @partner_id

        IF(@active_flag=1)
            BEGIN
                SELECT 'MESSAGE.website_partner_exist';
			 END;
            ELSE
            BEGIN
                IF(@active_flag=0)
                    BEGIN
                        UPDATE dbo.website_partner
                          SET 
                              		partner_logo = @partner_logo,
		partner_link = @partner_link,
		partner_name = @partner_name,
		sort_order = @sort_order,
		is_show = @is_show,
		created_by_user_id = @created_by_user_id,
lu_updated = GETDATE(),
  
                              active_flag = 1
                        WHERE website_partner.partner_id =@partner_id;
                       
                END;
                    ELSE
                    BEGIN
                        INSERT INTO dbo.website_partner
                        (
                          partner_id , partner_logo , partner_link , partner_name , sort_order , is_show , created_by_user_id , created_date_time , lu_updated , lu_user_id , active_flag 
                        )
                        VALUES
                        (
                          @partner_id , @partner_logo , @partner_link , @partner_name , @sort_order , @is_show , @created_by_user_id , GETDATE() , GETDATE() , @created_by_user_id , 1 
                        );
                       
                END;
		    END; 
		END TRY
	BEGIN CATCH 
		-- Set error code
		SET @OUT_ERR_CD  = @@ERROR;
		-- Set error message
		SET @OUT_ERR_MSG = ERROR_MESSAGE();
      
	END CATCH
    END;
-------------- Update -------------------------
CREATE PROCEDURE [dbo].[website_partner_update]
(@partner_id uniqueidentifier , @partner_logo varchar(200) , @partner_link varchar(200) , @partner_name varchar(150) , @sort_order int , @is_show bit , @lu_user_id uniqueidentifier ,
 @OUT_ERR_CD  INT OUTPUT  ,        
 @OUT_ERR_MSG NVARCHAR(MAX) OUTPUT 
)
AS
    BEGIN
		SET XACT_ABORT ON;
		SET NOCOUNT ON;
		SET @OUT_ERR_CD  = 0;
		SET @OUT_ERR_MSG = NULL;
		
		BEGIN TRY	
				UPDATE website_partner
		SET
		partner_logo = @partner_logo,
		partner_link = @partner_link,
		partner_name = @partner_name,
		sort_order = @sort_order,
		is_show = @is_show,
		lu_user_id = @lu_user_id,
lu_updated = GETDATE()
		WHERE 	partner_id = @partner_id;
			;
	END TRY
	BEGIN CATCH 
		-- Set error code
		SET @OUT_ERR_CD  = @@ERROR;
		-- Set error message
		SET @OUT_ERR_MSG = ERROR_MESSAGE();
      
	END CATCH
    END;
-------------- Search -------------------------
 CREATE PROCEDURE [dbo].[website_partner_search] 
(
@page_index   INT, 
@page_size    INT, 
@lang         CHAR(1)
 , @partner_name varchar(150),
@OUT_TOTAL_ROW  INT OUTPUT,
@OUT_ERR_CD  INT OUTPUT  ,        
@OUT_ERR_MSG NVARCHAR(MAX) OUTPUT 
)
AS
    BEGIN
		SET NOCOUNT ON;
		SET ARITHABORT ON
		SET @OUT_ERR_CD  = 0;
		SET @OUT_ERR_MSG = NULL;
		BEGIN TRY	
        IF(@page_size <> 0)
            BEGIN
                IF(@lang = 'e')
                    BEGIN
                        SELECT(ROW_NUMBER() OVER(
                              ORDER BY sort_order ASC)) AS RowNumber, 
                               	w.partner_id, 
	w.partner_logo, 
	w.partner_link, 
	w.partner_name, 
	w.sort_order, 
	w.is_show
                        INTO #Results1
                        FROM dbo.[website_partner] AS w
                        WHERE w.active_flag = 1  AND  ( @partner_name = '' OR w.partner_name LIKE ('%' + @partner_name + '%'))
						OPTION (OPTIMIZE FOR UNKNOWN)
                        SELECT @OUT_TOTAL_ROW = COUNT(*)
                        FROM #Results1;
                        SELECT *, 
                               @OUT_TOTAL_ROW AS RecordCount
                        FROM #Results1
                       	WHERE ROWNUMBER BETWEEN ((@page_index - 1) * @page_size) + 1 AND(@page_index*@page_size)

                        DROP TABLE #Results1;
                END;
                    ELSE
                    BEGIN
                        SELECT(ROW_NUMBER() OVER(
                              ORDER BY sort_order ASC)) AS RowNumber, 
                              	w.partner_id, 
	w.partner_logo, 
	w.partner_link, 
	w.partner_name, 
	w.sort_order, 
	w.is_show
                        INTO #Results2
                        FROM dbo.[website_partner] AS w
                        WHERE w.active_flag = 1  AND  ( @partner_name = '' OR w.partner_name LIKE ('%' + @partner_name + '%'))
						OPTION (OPTIMIZE FOR UNKNOWN)
                        
                        SELECT @OUT_TOTAL_ROW = COUNT(*)
                        FROM #Results2;

                        SELECT *, 
                               @OUT_TOTAL_ROW AS RecordCount
                        FROM #Results2
                       	WHERE ROWNUMBER BETWEEN ((@page_index - 1) * @page_size) + 1 AND(@page_index*@page_size)

                        DROP TABLE #Results2;
                END;
        END;
            ELSE
            BEGIN
                IF(@lang = 'e')
                    BEGIN
                        SELECT(ROW_NUMBER() OVER(
                              ORDER BY sort_order ASC)) AS RowNumber, 
                               	w.partner_id, 
	w.partner_logo, 
	w.partner_link, 
	w.partner_name, 
	w.sort_order, 
	w.is_show
                        INTO #Results3
                        FROM dbo.[website_partner] AS w
                        WHERE w.active_flag = 1  AND  ( @partner_name = '' OR w.partner_name LIKE ('%' + @partner_name + '%'))
						OPTION (OPTIMIZE FOR UNKNOWN)

                        SELECT @OUT_TOTAL_ROW = COUNT(*)
                        FROM #Results3;

                        SELECT *, 
                               @OUT_TOTAL_ROW AS RecordCount
                        FROM #Results3;

                        DROP TABLE #Results3;
                END;
                    ELSE
                    BEGIN
                        
                        SELECT(ROW_NUMBER() OVER(
                              ORDER BY sort_order ASC)) AS RowNumber, 
                              	w.partner_id, 
	w.partner_logo, 
	w.partner_link, 
	w.partner_name, 
	w.sort_order, 
	w.is_show
                        INTO #Results4
                        FROM dbo.[website_partner] AS w
                        WHERE w.active_flag = 1  AND  ( @partner_name = '' OR w.partner_name LIKE ('%' + @partner_name + '%'))
						OPTION (OPTIMIZE FOR UNKNOWN)

                        SELECT @OUT_TOTAL_ROW = COUNT(*)
                        FROM #Results4;

                        SELECT *, 
                               @OUT_TOTAL_ROW AS RecordCount
                        FROM #Results4;

                        DROP TABLE #Results4;
                END;
        END;
	END TRY
	BEGIN CATCH
		    
		-- Set error code
		SET @OUT_ERR_CD  = @@ERROR;
		-- Set error message
		SET @OUT_ERR_MSG = ERROR_MESSAGE();
      
	END CATCH
    END;
-------------- GetbyId -------------------------
CREATE PROCEDURE [dbo].[website_partner_get_by_id]
(
 @partner_id uniqueidentifier,
 @OUT_ERR_CD  INT OUTPUT  ,        
 @OUT_ERR_MSG NVARCHAR(MAX) OUTPUT 
)
AS
    BEGIN
		SET NOCOUNT ON;
		SET ARITHABORT ON
		SET @OUT_ERR_CD  = 0;
		SET @OUT_ERR_MSG = NULL;
		BEGIN TRY	
        SELECT 
        	w.partner_id,
	w.partner_logo,
	w.partner_link,
	w.partner_name,
	w.sort_order,
	w.is_show,
	w.created_by_user_id,
	w.created_date_time,
	w.lu_updated,
	w.lu_user_id,
	w.active_flag
        FROM dbo.[website_partner] AS w
        WHERE w.active_flag = 1 AND w.partner_id = @partner_id
		OPTION (OPTIMIZE FOR UNKNOWN)
	END TRY
	BEGIN CATCH
		-- Set error code
		SET @OUT_ERR_CD  = @@ERROR;
		-- Set error message
		SET @OUT_ERR_MSG = ERROR_MESSAGE();
	END CATCH
    END;
-------------- Delete -------------------------
CREATE PROCEDURE [dbo].[website_partner_delete_multi]
(@json_list_id NVARCHAR(MAX), 
 @updated_by   uniqueidentifier,
 @OUT_ERR_CD  INT OUTPUT  ,        
 @OUT_ERR_MSG NVARCHAR(MAX) OUTPUT 
)
AS
    BEGIN
		SET XACT_ABORT ON;
		SET NOCOUNT ON;
		SET ARITHABORT ON
		SET @OUT_ERR_CD  = 0;
		SET @OUT_ERR_MSG = NULL;
		BEGIN TRY	
				-- update thong tin website_partner
				UPDATE T1
					SET 
						T1.active_flag = 0, 
						T1.lu_user_id = @updated_by, 
						T1.lu_updated = GETDATE()
				FROM dbo.website_partner T1
				INNER JOIN (
					SELECT JSON_VALUE(p.value, '$.partner_id') AS partner_id
					FROM OPENJSON(@json_list_id) AS p
				) T2 on T1.partner_id=T2.partner_id
				
				--lay ra danh sach
				SELECT *
				FROM dbo.website_partner AS T
				INNER JOIN (
								SELECT JSON_VALUE(p.value, '$.partner_id') AS partner_id
								FROM OPENJSON(@json_list_id) AS p
							) tmp on T.partner_id=tmp.partner_id
				OPTION (OPTIMIZE FOR UNKNOWN)
	END TRY
	BEGIN CATCH
		-- Set error code
		SET @OUT_ERR_CD  = @@ERROR;
		-- Set error message
		SET @OUT_ERR_MSG = ERROR_MESSAGE();
	END CATCH
    END;
