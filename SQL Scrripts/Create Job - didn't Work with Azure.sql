EXEC dbo.sp_add_job  
    @job_name = N'Actualize YoY Data on daily base' ;  
GO  
EXEC sp_add_jobstep    
    @job_name = N'Actualize YoY Data on daily base',  
    @step_name = N'Executed Stored Procedure',  
    @subsystem = N'TSQL',  
    @command = N'EXEC CreateYOYReport',   
    @retry_attempts = 5,  
    @retry_interval = 5 ;  
GO  
EXEC dbo.sp_add_schedule  
    @schedule_name = N'RunOnce',  
    @freq_type = 16,  
    @active_start_time = 233000 ;  
USE msdb ;  
GO  
EXEC sp_attach_schedule  
   @job_name = N'Actualize YoY Data on daily base',  
   @schedule_name = N'RunOnce';  
GO  
EXEC dbo.sp_add_jobserver  
    @job_name = N'Actualize YoY Data on daily base';  
GO 