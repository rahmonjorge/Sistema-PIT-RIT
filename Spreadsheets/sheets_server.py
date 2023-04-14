from concurrent import futures
from datetime import datetime
import grpc, sheetsEditor_pb2_grpc, sheetsEditor_pb2, googleapi, random
from database import sheets_pb2, sheets_pb2_grpc 


channel = grpc.insecure_channel('26.145.102.198:6924')
stub = sheets_pb2_grpc.SpreadsheetServiceStub(channel)

(DRIVE_CLIENT,SPREADSHEET_CLIENT) = googleapi.getClients()

class SheetsService(sheetsEditor_pb2_grpc.SheetsServiceServicer):
    def GetPitPDF(self, request, context):
        """Missing associated documentation comment in .proto file."""
    
        getUser1 = sheets_pb2.GetUserRequest(id= request.userId)
    
        user = stub.GetUser(getUser1)
       
        getSheet1 = sheets_pb2.GetSheetRequest(userId=request.userId, type='pit', ano= request.ano)

        sheet = stub.GetSheet(getSheet1)

        sheetId = googleapi.copy_file(drive_client=DRIVE_CLIENT,file_id=googleapi.PLANILHA_PIT_ID,fileName=f'Planilha Pit - {user.name}',folder_id=googleapi.FOLDER_ID)

        popularPlanilhaPit(spreadsheetclient=SPREADSHEET_CLIENT,ano = request.ano, user = user, sheet=sheet, fileId=sheetId)

        pdf = googleapi.export_as_pdf(drive_client = DRIVE_CLIENT, doc_id = sheetId)

        DRIVE_CLIENT.files().delete(fileId=sheetId).execute()

        return sheetsEditor_pb2.PitAsPDF(fileName=f'PIT {request.ano}',pdf=pdf)

    def GetRitPDF(self, request, context):
        """Missing associated documentation comment in .proto file."""
    
        getUser1 = sheets_pb2.GetUserRequest(id= request.userId)
    
        user = stub.GetUser(getUser1)
       
        getSheetRit = sheets_pb2.GetSheetRequest(userId=request.userId, type='rit', ano= request.ano)
        
        getSheetPit = sheets_pb2.GetSheetRequest(userId=request.userId, type='pit', ano= request.ano)

        sheetRit = stub.GetSheet(getSheetRit)
        
        sheetPit = stub.GetSheet(getSheetPit)

        sheetId = googleapi.copy_file(drive_client=DRIVE_CLIENT,file_id=googleapi.PLANILHA_RIT_ID,fileName=f'Planilha Rit - {user.name}',folder_id=googleapi.FOLDER_ID)

        popularPlanilhaRit(spreadsheetclient=SPREADSHEET_CLIENT,ano = request.ano, user = user, sheet_pit=sheetPit, sheet_rit=sheetRit, fileId=sheetId)

        pdf = googleapi.export_as_pdf(drive_client = DRIVE_CLIENT, doc_id = sheetId)

        DRIVE_CLIENT.files().delete(fileId=sheetId).execute()

        return sheetsEditor_pb2.PitAsPDF(fileName=f'RIT {request.ano}',pdf=pdf)

def iniciarServidor():
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    sheetsEditor_pb2_grpc.add_SheetsServiceServicer_to_server(
      SheetsService(), server)
    server.add_insecure_port('[::]:50053')
    server.start()
    server.wait_for_termination()
    

def popularPlanilhaRit(spreadsheetclient, ano, user, sheet_pit, sheet_rit, fileId):
    ensino_pit = list(sheet_pit.ensino)
    ensino_pit.append(sheet_pit.ch_ensino)

    pesquisa_pit = list(sheet_pit.pesquisa)
    pesquisa_pit.append(sheet_pit.ch_pesquisa)

    extensao_pit = list(sheet_pit.extensao)
    extensao_pit.append(sheet_pit.ch_extensao)

    adm_pit = list(sheet_pit.adm)
    adm_pit.append(sheet_pit.ch_adm)

    ensino_rit = list(sheet_rit.ensino)
    ensino_rit.append(sheet_rit.ch_ensino)

    pesquisa_rit = list(sheet_rit.pesquisa)
    pesquisa_rit.append(sheet_rit.ch_pesquisa)

    extensao_rit = list(sheet_rit.extensao)
    extensao_rit.append(sheet_rit.ch_extensao)

    adm_rit = list(sheet_rit.adm)
    adm_rit.append(sheet_rit.ch_adm)

    body = {
        "valueInputOption": "USER_ENTERED",
        "data": [{
            'range': "C2:C9",
            'majorDimension': "COLUMNS",
            'values': [[
                ano,
                user.name,
                user.siape,
                user.departamento,
                user.email,
                user.vinculo,
                user.regime,
                user.reducao
            ]]},
            {
                'range': "I13:J14",
                'majorDimension': "COLUMNS",
                'values': [[
                    sheet_pit.ch_grad,
                    sheet_pit.ch_pos
                ], [
                    sheet_rit.ch_grad,
                    sheet_rit.ch_pos
                ]]
        },
            {
                'range': "I19:J35",
                'majorDimension': "COLUMNS",
                'values': [
                    ensino_pit,
                    ensino_rit
                ]
        },
            {
                'range': "I39:J68",
                'majorDimension': "COLUMNS",
                'values': [
                    pesquisa_pit,
                    pesquisa_rit
                ]
        },
            {
                'range': "I72:J90",
                'majorDimension': "COLUMNS",
                'values': [
                    extensao_pit,
                    extensao_rit
                ]
        },
            {
                'range': "I94:J105",
                'majorDimension': "COLUMNS",
                'values': [
                    adm_pit,
                    adm_rit
                ]
        },
                        {
                'range': "B111:B111",
                'majorDimension': "COLUMNS",
                'values': [
                    [f'Docente: {user.name}']
                ]
        },
            {
                'range': "B113:B113",
                'majorDimension': "COLUMNS",
                'values': [
                    [f'Recife, {datetime.now().strftime("%d/%m/%Y")}']
                ]
        }
        ]
    }
    spreadsheetclient.spreadsheets().values().batchUpdate(
        spreadsheetId=fileId, body=body).execute()

def popularPlanilhaPit(spreadsheetclient, ano, user, sheet, fileId):
    ensino = list(sheet.ensino)
    ensino.append(sheet.ch_ensino)

    pesquisa = list(sheet.pesquisa)
    pesquisa.append(sheet.ch_pesquisa)

    extensao = list(sheet.extensao)
    extensao.append(sheet.ch_extensao)

    adm = list(sheet.adm)
    adm.append(sheet.ch_adm)

    body = {
        "valueInputOption": "USER_ENTERED",
        "data": [{
            'range': "C2:C9",
            'majorDimension': "COLUMNS",
            'values': [[
                ano,
                user.name,
                user.siape,
                user.departamento,
                user.email,
                user.vinculo,
                user.regime,
                user.reducao
            ]]},
            {
                'range': "I13:I14",
                'majorDimension': "COLUMNS",
                'values': [[
                    sheet.ch_grad,
                    sheet.ch_pos
                ]]
        },
            {
                'range': "I19:I35",
                'majorDimension': "COLUMNS",
                'values': [
                    ensino
                ]
        },
            {
                'range': "I39:I68",
                'majorDimension': "COLUMNS",
                'values': [
                    pesquisa
                ]
        },
            {
                'range': "I72:I90",
                'majorDimension': "COLUMNS",
                'values': [
                    extensao
                ]
        },
            {
                'range': "I94:I105",
                'majorDimension': "COLUMNS",
                'values': [
                    adm
                ]
        },
            {
                'range': "B111:B111",
                'majorDimension': "COLUMNS",
                'values': [
                    [f'Docente: {user.name}']
                ]
        },
            {
                'range': "B113:B113",
                'majorDimension': "COLUMNS",
                'values': [
                    [f'Recife, {datetime.now().strftime("%d/%m/%Y")}']
                ]
        }
        ]
    }
    spreadsheetclient.spreadsheets().values().batchUpdate(spreadsheetId=fileId, body=body).execute()


if __name__=='__main__':
    iniciarServidor()
