# User Stories

The ideal is that there should be one use case class per user story.

There are various other concerns like performance that will impacts this.

|No.| Actors | Story                                                      | Acceptance Criteria |
|---|:------:|:-----------------------------------------------------------|---------------------|
|1  | User   | As a user I need to be able to create a task.              | A task is created with a summary, optional description and due date |
|2  | User   | As a user I need to be able to schedule tasks so I can plan my workload | Tasks can be assigned a scheduled date |
|3  | User   | As a user I want to know what tasks I need to do today.    | A list of tasks scheduled for today.  |
|4  | User   | As a user I need to know when my tasks are due so I can plan my time.| Each task is able to be assigned a due date. |
|5  | User   | As a user I need to know what tasks I have completed.      | View completed tasks. |
|6  | User   | As a user I need to authenticate with the system to validate my identity| Provide valid username and password to gain access to the system|
|7  | Client | As a client I want to use my existing OIDC complaint provider to authenticate users| Configure a OIDC provider to authenticate users. | 
|8  | Client | As a client I want all registration of users to be handled by the OIDC provider | The system adds authenticated users if they don't exist. |