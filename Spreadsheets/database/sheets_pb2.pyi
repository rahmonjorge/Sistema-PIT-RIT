from google.protobuf.internal import containers as _containers
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from typing import ClassVar as _ClassVar, Iterable as _Iterable, Optional as _Optional

DESCRIPTOR: _descriptor.FileDescriptor

class GetSheetRequest(_message.Message):
    __slots__ = ["ano", "type", "userId"]
    ANO_FIELD_NUMBER: _ClassVar[int]
    TYPE_FIELD_NUMBER: _ClassVar[int]
    USERID_FIELD_NUMBER: _ClassVar[int]
    ano: int
    type: str
    userId: str
    def __init__(self, userId: _Optional[str] = ..., type: _Optional[str] = ..., ano: _Optional[int] = ...) -> None: ...

class GetUserRequest(_message.Message):
    __slots__ = ["id"]
    ID_FIELD_NUMBER: _ClassVar[int]
    id: str
    def __init__(self, id: _Optional[str] = ...) -> None: ...

class SheetResponse(_message.Message):
    __slots__ = ["adm", "ch_adm", "ch_ensino", "ch_extensao", "ch_grad", "ch_pesquisa", "ch_pos", "ensino", "extensao", "id", "pesquisa"]
    ADM_FIELD_NUMBER: _ClassVar[int]
    CH_ADM_FIELD_NUMBER: _ClassVar[int]
    CH_ENSINO_FIELD_NUMBER: _ClassVar[int]
    CH_EXTENSAO_FIELD_NUMBER: _ClassVar[int]
    CH_GRAD_FIELD_NUMBER: _ClassVar[int]
    CH_PESQUISA_FIELD_NUMBER: _ClassVar[int]
    CH_POS_FIELD_NUMBER: _ClassVar[int]
    ENSINO_FIELD_NUMBER: _ClassVar[int]
    EXTENSAO_FIELD_NUMBER: _ClassVar[int]
    ID_FIELD_NUMBER: _ClassVar[int]
    PESQUISA_FIELD_NUMBER: _ClassVar[int]
    adm: _containers.RepeatedScalarFieldContainer[bool]
    ch_adm: float
    ch_ensino: float
    ch_extensao: float
    ch_grad: float
    ch_pesquisa: float
    ch_pos: float
    ensino: _containers.RepeatedScalarFieldContainer[bool]
    extensao: _containers.RepeatedScalarFieldContainer[bool]
    id: str
    pesquisa: _containers.RepeatedScalarFieldContainer[bool]
    def __init__(self, id: _Optional[str] = ..., ch_grad: _Optional[float] = ..., ch_pos: _Optional[float] = ..., ensino: _Optional[_Iterable[bool]] = ..., ch_ensino: _Optional[float] = ..., pesquisa: _Optional[_Iterable[bool]] = ..., ch_pesquisa: _Optional[float] = ..., extensao: _Optional[_Iterable[bool]] = ..., ch_extensao: _Optional[float] = ..., adm: _Optional[_Iterable[bool]] = ..., ch_adm: _Optional[float] = ...) -> None: ...

class UserResponse(_message.Message):
    __slots__ = ["departamento", "email", "id", "name", "reducao", "regime", "siape", "vinculo"]
    DEPARTAMENTO_FIELD_NUMBER: _ClassVar[int]
    EMAIL_FIELD_NUMBER: _ClassVar[int]
    ID_FIELD_NUMBER: _ClassVar[int]
    NAME_FIELD_NUMBER: _ClassVar[int]
    REDUCAO_FIELD_NUMBER: _ClassVar[int]
    REGIME_FIELD_NUMBER: _ClassVar[int]
    SIAPE_FIELD_NUMBER: _ClassVar[int]
    VINCULO_FIELD_NUMBER: _ClassVar[int]
    departamento: str
    email: str
    id: str
    name: str
    reducao: str
    regime: str
    siape: str
    vinculo: str
    def __init__(self, id: _Optional[str] = ..., email: _Optional[str] = ..., name: _Optional[str] = ..., siape: _Optional[str] = ..., departamento: _Optional[str] = ..., vinculo: _Optional[str] = ..., regime: _Optional[str] = ..., reducao: _Optional[str] = ...) -> None: ...
