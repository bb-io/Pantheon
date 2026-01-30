# Blackbird.io welocalize 

Blackbird is the new automation backbone for the language technology industry. Blackbird provides enterprise-scale automation and orchestration with a simple no-code/low-code platform. Blackbird enables ambitious organizations to identify, vet and automate as many processes as possible. Not just localization workflows, but any business and IT process. This repository represents an application that is deployable on Blackbird and usable inside the workflow editor.

## Introduction

<!-- begin docs -->

welocalize's system exposes a RESTful API, referred to as the Hypnos Client API, designed to manage translation workflows for enterprise clients. It supports standard content localization operations including submission of translation jobs, file uploads, status tracking, and retrieval of translated assets.

## Before setting up

Before you can connect, make sure you have a welocalize authentication token.

## Connecting

1. Name your connection for future reference e.g. 'My organization'.
2. Paste your authentication key.
3. Choose the base URL: either the 'Sandbox' or 'Production' one.
4. Click _Authorize connection_ and wait for the process to complete.
5. Confirm that the connection has appeared and its status is _Connected_.

![connecting](image/README/connection.png)

## Actions

### Files

- **Upload file**. Upload a file to a created project.
- **Download target file**. Download a file for a target locale from a specific project. If the project deliverable is a hyperlink, it returns the URL, name and ID without any files.
- **Search deliverables**. Get a list of deliverable files for a specific project.
- **Delete file**. Delete a specific file from a project.
- **Search assets**. Get a list of assets for a specific project.

### Projects

- **Search projects**. Search all projects.
- **Get project status**. Get status for a specific project.
- **Create project**. Create a new project.
- **Start project**. Start a project with a specific ID.

## Events

- **On project status changed**. Triggers when the status of a specified project changes.

## Example bird

![bird](image/README/bird.png)

## Feedback

Do you want to use this app or do you have feedback on our implementation? Reach out to us using the [established channels](https://www.blackbird.io/) or create an issue.

<!-- end docs -->
