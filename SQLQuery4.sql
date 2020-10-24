alter table SubmissionMaster add  JobDescription varchar(max) null,
	InterviewStatus varchar(max) null,
	InterviewDate date null,
	AssingedTo varchar(100) not null,
	InterviewTime time null,
	InterviewFeedBack varchar(max) not null