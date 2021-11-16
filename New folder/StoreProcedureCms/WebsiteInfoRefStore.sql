-------------- Insert -------------------------
create PROCEDURE [dbo].[website_info_ref_create]
(@web_info_rcd varchar(50) , @web_info_logo_l varchar(250) , @web_info_logo_e varchar(250) , @web_info_slogan_l nvarchar(100) , @web_info_slogan_e nvarchar(100) , @web_info_faculty_e nvarchar(100) , @web_info_faculty_l nvarchar(100) , @web_info_address nvarchar(200) , @web_info_email varchar(100) , @web_info_phone varchar(20) , @web_info_website varchar(200) , @web_info_facebook varchar(200) , @web_info_zalo varchar(200) , @web_info_youtube varchar(200) , @sort_order int , @created_by_user_id uniqueidentifier ,
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
		FROM dbo.website_info_ref w
		WHERE w.web_info_rcd = @web_info_rcd

        IF(@active_flag=1)
            BEGIN
                SELECT 'MESSAGE.website_info_ref_exist';
			 END;
            ELSE
            BEGIN
                IF(@active_flag=0)
                    BEGIN
                        UPDATE dbo.website_info_ref
                          SET 
                              		web_info_logo_l = @web_info_logo_l,
		web_info_logo_e = @web_info_logo_e,
		web_info_slogan_l = @web_info_slogan_l,
		web_info_slogan_e = @web_info_slogan_e,
		web_info_faculty_e = @web_info_faculty_e,
		web_info_faculty_l = @web_info_faculty_l,
		web_info_address = @web_info_address,
		web_info_email = @web_info_email,
		web_info_phone = @web_info_phone,
		web_info_website = @web_info_website,
		web_info_facebook = @web_info_facebook,
		web_info_zalo = @web_info_zalo,
		web_info_youtube = @web_info_youtube,
		sort_order = @sort_order,
		created_by_user_id = @created_by_user_id,
lu_updated = GETDATE(),
  
                              active_flag = 1
                        WHERE website_info_ref.web_info_rcd =@web_info_rcd;
                       
                END;
                    ELSE
                    BEGIN
                        INSERT INTO dbo.website_info_ref
                        (
                          web_info_rcd , web_info_logo_l , web_info_logo_e , web_info_slogan_l , web_info_slogan_e , web_info_faculty_e , web_info_faculty_l , web_info_address , web_info_email , web_info_phone , web_info_website , web_info_facebook , web_info_zalo , web_info_youtube , sort_order , created_by_user_id , created_date_time , lu_updated , lu_user_id , active_flag 
                        )
                        VALUES
                        (
                          @web_info_rcd , @web_info_logo_l , @web_info_logo_e , @web_info_slogan_l , @web_info_slogan_e , @web_info_faculty_e , @web_info_faculty_l , @web_info_address , @web_info_email , @web_info_phone , @web_info_website , @web_info_facebook , @web_info_zalo , @web_info_youtube , @sort_order , @created_by_user_id , GETDATE() , GETDATE() , @created_by_user_id , 1 
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
CREATE PROCEDURE [dbo].[website_info_ref_update]
(@web_info_rcd varchar(50) , @web_info_logo_l varchar(250) , @web_info_logo_e varchar(250) , @web_info_slogan_l nvarchar(100) , @web_info_slogan_e nvarchar(100) , @web_info_faculty_e nvarchar(100) , @web_info_faculty_l nvarchar(100) , @web_info_address nvarchar(200) , @web_info_email varchar(100) , @web_info_phone varchar(20) , @web_info_website varchar(200) , @web_info_facebook varchar(200) , @web_info_zalo varchar(200) , @web_info_youtube varchar(200) , @sort_order int , @lu_user_id uniqueidentifier ,
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
		IF(NOT EXISTS(SELECT 1 FROM website_info_ref AS w WHERE w.web_info_rcd = @web_info_rcd AND w.active_flag = 1))
BEGIN
 SELECT 'MESSAGE.web_info_rcd_not_exist';
END; ELSE 
 BEGIN 
		UPDATE website_info_ref
		SET
		web_info_logo_l = @web_info_logo_l,
		web_info_logo_e = @web_info_logo_e,
		web_info_slogan_l = @web_info_slogan_l,
		web_info_slogan_e = @web_info_slogan_e,
		web_info_faculty_e = @web_info_faculty_e,
		web_info_faculty_l = @web_info_faculty_l,
		web_info_address = @web_info_address,
		web_info_email = @web_info_email,
		web_info_phone = @web_info_phone,
		web_info_website = @web_info_website,
		web_info_facebook = @web_info_facebook,
		web_info_zalo = @web_info_zalo,
		web_info_youtube = @web_info_youtube,
		sort_order = @sort_order,
		lu_user_id = @lu_user_id,
lu_updated = GETDATE()
		WHERE 	web_info_rcd = @web_info_rcd;
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
 CREATE PROCEDURE [dbo].[website_info_ref_search] 
(
@page_index   INT, 
@page_size    INT, 
@lang         CHAR(1)
 , @web_info_rcd varchar(50) , @web_info_faculty nvarchar(100),
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
                               	w.web_info_faculty_e web_info_faculty, 
	w.web_info_rcd, 
	w.web_info_address, 
	w.web_info_email, 
	w.web_info_phone, 
	w.sort_order
                        INTO #Results1
                        FROM dbo.[website_info_ref] AS w
                        WHERE w.active_flag = 1  AND (@web_info_faculty = '' OR w.web_info_faculty_e LIKE ('%' + @web_info_faculty + '%')) AND  ( @web_info_rcd = '' OR w.web_info_rcd LIKE ('%' + @web_info_rcd + '%'))
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
                              	w.web_info_faculty_l web_info_faculty, 
	w.web_info_rcd, 
	w.web_info_address, 
	w.web_info_email, 
	w.web_info_phone, 
	w.sort_order
                        INTO #Results2
                        FROM dbo.[website_info_ref] AS w
                        WHERE w.active_flag = 1  AND (@web_info_faculty = '' OR w.web_info_faculty_l LIKE ('%' + @web_info_faculty + '%')) AND  ( @web_info_rcd = '' OR w.web_info_rcd LIKE ('%' + @web_info_rcd + '%'))
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
                               	w.web_info_faculty_e web_info_faculty, 
	w.web_info_rcd, 
	w.web_info_address, 
	w.web_info_email, 
	w.web_info_phone, 
	w.sort_order
                        INTO #Results3
                        FROM dbo.[website_info_ref] AS w
                        WHERE w.active_flag = 1  AND (@web_info_faculty = '' OR w.web_info_faculty_e LIKE ('%' + @web_info_faculty + '%')) AND  ( @web_info_rcd = '' OR w.web_info_rcd LIKE ('%' + @web_info_rcd + '%'))
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
                              	w.web_info_faculty_l web_info_faculty, 
	w.web_info_rcd, 
	w.web_info_address, 
	w.web_info_email, 
	w.web_info_phone, 
	w.sort_order
                        INTO #Results4
                        FROM dbo.[website_info_ref] AS w
                        WHERE w.active_flag = 1  AND (@web_info_faculty = '' OR w.web_info_faculty_l LIKE ('%' + @web_info_faculty + '%')) AND  ( @web_info_rcd = '' OR w.web_info_rcd LIKE ('%' + @web_info_rcd + '%'))
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
CREATE PROCEDURE [dbo].[website_info_ref_get_by_id]
(
 @web_info_rcd varchar(50),
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
        	w.web_info_rcd,
	w.web_info_logo_l,
	w.web_info_logo_e,
	w.web_info_slogan_l,
	w.web_info_slogan_e,
	w.web_info_faculty_e,
	w.web_info_faculty_l,
	w.web_info_address,
	w.web_info_email,
	w.web_info_phone,
	w.web_info_website,
	w.web_info_facebook,
	w.web_info_zalo,
	w.web_info_youtube,
	w.sort_order,
	w.created_by_user_id,
	w.created_date_time,
	w.lu_updated,
	w.lu_user_id,
	w.active_flag
        FROM dbo.[website_info_ref] AS w
        WHERE w.active_flag = 1 AND w.web_info_rcd = @web_info_rcd
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
CREATE PROCEDURE [dbo].[website_info_ref_delete_multi]
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
				-- update thong tin website_info_ref
				UPDATE T1
					SET 
						T1.active_flag = 0, 
						T1.lu_user_id = @updated_by, 
						T1.lu_updated = GETDATE()
				FROM dbo.website_info_ref T1
				INNER JOIN (
					SELECT JSON_VALUE(p.value, '$.web_info_rcd') AS web_info_rcd
					FROM OPENJSON(@json_list_id) AS p
				) T2 on T1.web_info_rcd=T2.web_info_rcd
				
				--lay ra danh sach
				SELECT *
				FROM dbo.website_info_ref AS T
				INNER JOIN (
								SELECT JSON_VALUE(p.value, '$.web_info_rcd') AS web_info_rcd
								FROM OPENJSON(@json_list_id) AS p
							) tmp on T.web_info_rcd=tmp.web_info_rcd
				OPTION (OPTIMIZE FOR UNKNOWN)
	END TRY
	BEGIN CATCH
		-- Set error code
		SET @OUT_ERR_CD  = @@ERROR;
		-- Set error message
		SET @OUT_ERR_MSG = ERROR_MESSAGE();
	END CATCH
    END;
