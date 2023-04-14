from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from typing import ClassVar as _ClassVar, Optional as _Optional

DESCRIPTOR: _descriptor.FileDescriptor

class GetPitPDFRequest(_message.Message):
    __slots__ = ["ano", "userId"]
    ANO_FIELD_NUMBER: _ClassVar[int]
    USERID_FIELD_NUMBER: _ClassVar[int]
    ano: int
    userId: str
    def __init__(self, userId: _Optional[str] = ..., ano: _Optional[int] = ...) -> None: ...

class GetRitPDFRequest(_message.Message):
    __slots__ = ["ano", "userId"]
    ANO_FIELD_NUMBER: _ClassVar[int]
    USERID_FIELD_NUMBER: _ClassVar[int]
    ano: int
    userId: str
    def __init__(self, userId: _Optional[str] = ..., ano: _Optional[int] = ...) -> None: ...

class PitAsPDF(_message.Message):
    __slots__ = ["fileName", "pdf"]
    FILENAME_FIELD_NUMBER: _ClassVar[int]
    PDF_FIELD_NUMBER: _ClassVar[int]
    fileName: str
    pdf: bytes
    def __init__(self, fileName: _Optional[str] = ..., pdf: _Optional[bytes] = ...) -> None: ...

class RitAsPDF(_message.Message):
    __slots__ = ["fileName", "pdf"]
    FILENAME_FIELD_NUMBER: _ClassVar[int]
    PDF_FIELD_NUMBER: _ClassVar[int]
    fileName: str
    pdf: bytes
    def __init__(self, fileName: _Optional[str] = ..., pdf: _Optional[bytes] = ...) -> None: ...
