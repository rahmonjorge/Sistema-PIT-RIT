# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: database.sheets.proto
"""Generated protocol buffer code."""
from google.protobuf.internal import builder as _builder
from google.protobuf import descriptor as _descriptor
from google.protobuf import descriptor_pool as _descriptor_pool
from google.protobuf import symbol_database as _symbol_database
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor_pool.Default().AddSerializedFile(b'\n\x15\x64\x61tabase.sheets.proto\x12\x0f\x64\x61tabase.sheets\"\x1c\n\x0eGetUserRequest\x12\n\n\x02id\x18\x01 \x01(\t\"\x8e\x01\n\x0cUserResponse\x12\n\n\x02id\x18\x01 \x01(\t\x12\r\n\x05\x65mail\x18\x02 \x01(\t\x12\x0c\n\x04name\x18\x03 \x01(\t\x12\r\n\x05siape\x18\x04 \x01(\t\x12\x14\n\x0c\x64\x65partamento\x18\x05 \x01(\t\x12\x0f\n\x07vinculo\x18\x06 \x01(\t\x12\x0e\n\x06regime\x18\x07 \x01(\t\x12\x0f\n\x07reducao\x18\x08 \x01(\t\"<\n\x0fGetSheetRequest\x12\x0e\n\x06userId\x18\x01 \x01(\t\x12\x0c\n\x04type\x18\x02 \x01(\t\x12\x0b\n\x03\x61no\x18\x03 \x01(\x05\"\xca\x01\n\rSheetResponse\x12\n\n\x02id\x18\x01 \x01(\t\x12\x0f\n\x07\x63h_grad\x18\x02 \x01(\x02\x12\x0e\n\x06\x63h_pos\x18\x03 \x01(\x02\x12\x0e\n\x06\x65nsino\x18\x04 \x03(\x08\x12\x11\n\tch_ensino\x18\x05 \x01(\x02\x12\x10\n\x08pesquisa\x18\x06 \x03(\x08\x12\x13\n\x0b\x63h_pesquisa\x18\x07 \x01(\x02\x12\x10\n\x08\x65xtensao\x18\x08 \x03(\x08\x12\x13\n\x0b\x63h_extensao\x18\t \x01(\x02\x12\x0b\n\x03\x61\x64m\x18\n \x03(\x08\x12\x0e\n\x06\x63h_adm\x18\x0b \x01(\x02\x32\xb1\x01\n\x12SpreadsheetService\x12K\n\x07GetUser\x12\x1f.database.sheets.GetUserRequest\x1a\x1d.database.sheets.UserResponse\"\x00\x12N\n\x08GetSheet\x12 .database.sheets.GetSheetRequest\x1a\x1e.database.sheets.SheetResponse\"\x00\x62\x06proto3')

_builder.BuildMessageAndEnumDescriptors(DESCRIPTOR, globals())
_builder.BuildTopDescriptorsAndMessages(DESCRIPTOR, 'database.sheets_pb2', globals())
if _descriptor._USE_C_DESCRIPTORS == False:

  DESCRIPTOR._options = None
  _GETUSERREQUEST._serialized_start=42
  _GETUSERREQUEST._serialized_end=70
  _USERRESPONSE._serialized_start=73
  _USERRESPONSE._serialized_end=215
  _GETSHEETREQUEST._serialized_start=217
  _GETSHEETREQUEST._serialized_end=277
  _SHEETRESPONSE._serialized_start=280
  _SHEETRESPONSE._serialized_end=482
  _SPREADSHEETSERVICE._serialized_start=485
  _SPREADSHEETSERVICE._serialized_end=662
# @@protoc_insertion_point(module_scope)