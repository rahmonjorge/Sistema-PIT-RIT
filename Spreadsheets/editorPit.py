from concurrent import futures
import grpc, sheetsEditor_pb2_grpc, sheetsEditor_pb2, googleapi, random
from database import sheets_pb2, sheets_pb2_grpc 


channel = grpc.insecure_channel('26.52.183.15:6924')
stub = sheets_pb2_grpc.SpreadsheetServiceStub(channel)

(DRIVE_CLIENT,SPREADSHEET_CLIENT) = googleapi.getClients()

class SheetsService(sheetsEditor_pb2_grpc.SheetsServiceServicer):

    def GetPitPDF(self, request, context):
        """Missing associated documentation comment in .proto file."""
    
        getUser1 = sheets_pb2.GetUserRequest(id= request.userId)
    
        user = stub.GetUser(getUser1)
       
        getSheet1 = sheets_pb2.GetSheetRequest(userId=request.userId, type='pit', ano= request.ano)

        sheet = stub.GetSheet(getSheet1)

        sheetId = googleapi.copy_file(drive_client=DRIVE_CLIENT,file_id=googleapi.PLANILHA_ID,fileName=f'Planilha Pit - {user.name}',folder_id=googleapi.FOLDER_ID)

        popularPlanilha(spreadsheetclient=SPREADSHEET_CLIENT,ano = request.ano, user = user, sheet=sheet, fileId=sheetId)

        pdf = googleapi.export_as_pdf(drive_client = DRIVE_CLIENT, doc_id = sheetId)

        DRIVE_CLIENT.files().delete(fileId=sheetId).execute()

        return sheetsEditor_pb2.PitAsPDF(fileName=f'PIT {request.ano}',pdf=pdf)

def iniciarServidor():
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    sheetsEditor_pb2_grpc.add_SheetsServiceServicer_to_server(
      SheetsService(), server)
    server.add_insecure_port('[::]:50053')
    server.start()
    server.wait_for_termination()
    


def popularPlanilha(spreadsheetclient, ano, user, sheet, fileId):
    ensino = list(sheet.ensino)
    ensino.append(sheet.ch_ensino)

    pesquisa = list(sheet.pesquisa)
    pesquisa.append(sheet.ch_pesquisa)

    extensao = list(sheet.extensao)
    extensao.append(sheet.ch_extensao)

    adm = list(sheet.adm)
    adm.append(sheet.ch_adm)

    print(sheet.ch_grad) 
    print(sheet.ch_pos)

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
        }
        ]
    }
    spreadsheetclient.spreadsheets().values().batchUpdate(spreadsheetId=fileId, body=body).execute()



if __name__=='__main__':
    iniciarServidor()
