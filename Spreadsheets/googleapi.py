from __future__ import print_function
import os.path
from google.auth.transport.requests import Request
from google.oauth2.credentials import Credentials
from google_auth_oauthlib.flow import InstalledAppFlow
from googleapiclient.discovery import build
from googleapiclient.errors import HttpError

# If modifying these scopes, delete the file token.json.
SCOPES = ['https://www.googleapis.com/auth/drive', 'https://www.googleapis.com/auth/drive.file','https://www.googleapis.com/auth/spreadsheets']

FOLDER_ID = '1Llwkw-E59cqfln2NCIz3bJgwWezFUVKe'
# The ID of a sample document.
PLANILHA_PIT_ID = '1e6kvNSLUHF1VfWg7Jvqmroc2zJi2J2vQHrcD0_oz128'
PLANILHA_RIT_ID = '1gY85_cd_pWGiACzrrMbLnZcbqjzszRYPMW0nrSytf94'

def export_as_pdf(drive_client, doc_id):
    """
    Exporta um documento como PDF e move para uma pasta (folder_id)
    """
    name = drive_client.files().get(fileId=doc_id).execute()['name']
    name += '.pdf'

    data =drive_client.files().export(fileId=doc_id,
                                       mimeType='application/pdf').execute()
    return data

def copy_file(drive_client, file_id, fileName, folder_id):
    """
    Copia um arquivo para uma pasta
    """
    newFileId = drive_client.files().copy(fileId=file_id, body={'name': fileName}).execute().get('id')
    file = drive_client.files().get(fileId=newFileId, fields='parents').execute()
    previous_parents = ','.join(file.get('parents'))
    drive_client.files().update(fileId=file_id,
                                       addParents=folder_id,
                                       removeParents=previous_parents,
                                       fields='id, parents').execute()
    return newFileId

def getClients():

    creds = None

    if os.path.exists('token.json'):
        creds = Credentials.from_authorized_user_file('token.json', SCOPES)
    # If there are no (valid) credentials available, let the user log in.
    if not creds or not creds.valid:
        if creds and creds.expired and creds.refresh_token:
            creds.refresh(Request())
        else:
            flow = InstalledAppFlow.from_client_secrets_file(
                'credentials.json', SCOPES)
            creds = flow.run_local_server(port=0)
        # Save the credentials for the next run
        with open('token.json', 'w') as token:
            token.write(creds.to_json())

    try:
        driveService = build('drive', 'v3',credentials=creds)
        sheetService = build('sheets','v4',credentials=creds)
        return driveService,sheetService
    
    except HttpError as err:
        print(err)
