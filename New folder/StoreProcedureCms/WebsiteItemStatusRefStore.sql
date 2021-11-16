-------------- Insert -------------------------
create PROCEDURE [dbo].[website_item_status_ref_create]
(@item_status_rcd varchar(50) , @item_status_name_e nvarchar(250) , @item_status_name_l nvarchar(250) , @sort_order int , @created_by_user_id uniqueidentifier ,
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
		FROM dbo.website_item_status_ref w
		WHERE w.item_status_rcd = @item_status_rcd

        IF(@active_flag=1)
            BEGIN
                SELECT 'MESSAGE.website_item_status_ref_exist';
			 END;
            ELSE
            BEGIN
                IF(@active_flag=0)
                    BEGIN
                        UPDATE dbo.website_item_status_ref
                          SET 
                              		item_status_name_e = @item_status_name_e,
		item_status_name_l = @item_status_name_l,
		sort_order = @sort_order,
		created_by_user_id = @created_by_user_id,
lu_updated = GETDATE(),
  
                              active_flag = 1
                        WHERE website_item_status_ref.item_status_rcd =@item_status_rcd;
                       
                END;
                    ELSE
                    BEGIN
                        INSERT INTO dbo.website_item_status_ref
                        (
                          item_status_rcd , item_status_name_e , item_status_name_l , sort_order , created_by_user_id , created_date_time , lu_updated , lu_user_id , active_flag 
                        )
                        VALUES
                        (
                          @item_status_rcd , @item_status_name_e , @item_status_name_l , @sort_order , @created_by_user_id , GETDATE() , GETDATE() , @created_by_user_id , 1 
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
CREATE PROCEDURE [dbo].[website_item_status_ref_update]
(@item_status_rcd varchar(50) , @item_status_name_e nvarchar(250) , @item_status_name_l nvarchar(250) , @sort_order int , @lu_user_id uniqueidentifier ,
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
		IF(NOT EXISTS(SELECT 1 FROM website_item_status_ref AS w WHERE w.item_status_rcd = @item_status_rcd AND w.active_flag = 1))
BEGIN
 SELECT 'MESSAGE.item_status_rcd_not_exist';
END; ELSE 
 BEGIN 
		UPDATE website_item_status_ref
		SET
		item_status_name_e = @item_status_name_e,
		item_status_name_l = @item_status_name_l,
		sort_order = @sort_order,
		lu_user_id = @lu_user_id,
lu_updated = GETDATE()
		WHERE 	item_status_rcd = @item_status_rcd;
			END;
	END TRY
	BEGIN CATCH 
		-- Set error code
		SET @OUT_ERR_CD  = @@ERROR;
		-- Set error message
		SET @OUT_ERR_MSG = ERROR_MESSAGE();
      
	END CATCH
    END;
-------------- Search -------------------------
 CREATE PROCEDURE [dbo].[website_item_status_ref_search] 
(
@page_index   INT, 
@page_size    INT, 
@lang         CHAR(1)
 , @item_status_name nvarchar(250),
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
                               	w.item_status_name_e item_status_name, 
	w.item_status_rcd, 
	w.sort_order
                        INTO #Results1
                        FROM dbo.[website_item_status_ref] AS w
                        WHERE w.active_flag = 1  AND (@item_status_name = '' OR w.item_status_name_e LIKE ('%' + @item_status_name + '%'))
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
                              	w.item_status_name_l item_status_name, 
	w.item_status_rcd, 
	w.sort_order
                        INTO #Results2
                        FROM dbo.[website_item_status_ref] AS w
                        WHERE w.active_flag = 1  AND (@item_status_name = '' OR w.item_status_name_l LIKE ('%' + @item_status_name + '%'))
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
                               	w.item_status_name_e item_status_name, 
	w.item_status_rcd, 
	w.sort_order
                        INTO #Results3
                        FROM dbo.[website_item_status_ref] AS w
                        WHERE w.active_flag = 1  AND (@item_status_name = '' OR w.item_status_name_e LIKE ('%' + @item_status_name + '%'))
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
                              	w.item_status_name_l item_status_name, 
	w.item_status_rcd, 
	w.sort_order
                        INTO #Results4
                        FROM dbo.[website_item_status_ref] AS w
                        WHERE w.active_flag = 1  AND (@item_status_name = '' OR w.item_status_name_l LIKE ('%' + @item_status_name + '%'))
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
CREATE PROCEDURE [dbo].[website_item_status_ref_get_by_id]
(
 @item_status_rcd varchar(50),
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
        	w.item_status_rcd,
	w.item_status_name_e,
	w.item_status_name_l,
	w.sort_order,
	w.created_by_user_id,
	w.created_date_time,
	w.lu_updated,
	w.lu_user_id,
	w.active_flag
        FROM dbo.[website_item_status_ref] AS w
        WHERE w.active_flag = 1 AND w.item_status_rcd = @item_status_rcd
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
CREATE PROCEDURE [dbo].[website_item_status_ref_delete_multi]
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
				-- update thong tin website_item_status_ref
				UPDATE T1
					SET 
						T1.active_flag = 0, 
						T1.lu_user_id = @updated_by, 
						T1.lu_updated = GETDATE()
				FROM dbo.website_item_status_ref T1
				INNER JOIN (
					SELECT JSON_VALUE(p.value, '$.item_status_rcd') AS item_status_rcd
					FROM OPENJSON(@json_list_id) AS p
				) T2 on T1.item_status_rcd=T2.item_status_rcd
				
				--lay ra danh sach
				SELECT *
				FROM dbo.website_item_status_ref AS T
				INNER JOIN (
								SELECT JSON_VALUE(p.value, '$.item_status_rcd') AS item_status_rcd
								FROM OPENJSON(@json_list_id) AS p
							) tmp on T.item_status_rcd=tmp.item_status_rcd
				OPTION (OPTIMIZE FOR UNKNOWN)
	END TRY
	BEGIN CATCH
		-- Set error code
		SET @OUT_ERR_CD  = @@ERROR;
		-- Set error message
		SET @OUT_ERR_MSG = ERROR_MESSAGE();
	END CATCH
    END;
-------------- GetListDropdown -------------------------
CREATE PROCEDURE [dbo].[website_item_status_ref_get_list_dropdown]
(
 @lang CHAR,
 @OUT_ERR_CD  INT OUTPUT,        
 @OUT_ERR_MSG NVARCHAR(MAX) OUTPUT 
)
AS
    BEGIN
		SET NOCOUNT ON;
		SET ARITHABORT ON
		SET @OUT_ERR_CD  = 0;
		SET @OUT_ERR_MSG = NULL;
		BEGIN TRY	
			IF(@lang = 'e')
				BEGIN
					SELECT
					  	w.item_status_name_e label,
	w.item_status_rcd value
,w.sort_order sort_order
					FROM dbo.[website_item_status_ref] AS w
					WHERE w.active_flag = 1 
					ORDER BY sort_order ASC, "item_status_name_e" ASC;
					
			END;
				ELSE
				BEGIN
					 SELECT
						w.item_status_name_l label,
	w.item_status_rcd value
,w.sort_order sort_order
					FROM dbo.[website_item_status_ref] AS w
					WHERE w.active_flag = 1 
					ORDER BY sort_order ASC, "item_status_name_l" ASC;
			END;
	END TRY
	BEGIN CATCH
		-- Set error code
		SET @OUT_ERR_CD  = @@ERROR;
		-- Set error message
		SET @OUT_ERR_MSG = ERROR_MESSAGE();
	END CATCH
    END;
