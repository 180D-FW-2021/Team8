# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: mediapipe/calculators/video/box_detector_calculator.proto
"""Generated protocol buffer code."""
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()


from mediapipe.framework import calculator_pb2 as mediapipe_dot_framework_dot_calculator__pb2
try:
  mediapipe_dot_framework_dot_calculator__options__pb2 = mediapipe_dot_framework_dot_calculator__pb2.mediapipe_dot_framework_dot_calculator__options__pb2
except AttributeError:
  mediapipe_dot_framework_dot_calculator__options__pb2 = mediapipe_dot_framework_dot_calculator__pb2.mediapipe.framework.calculator_options_pb2
from mediapipe.util.tracking import box_detector_pb2 as mediapipe_dot_util_dot_tracking_dot_box__detector__pb2


DESCRIPTOR = _descriptor.FileDescriptor(
  name='mediapipe/calculators/video/box_detector_calculator.proto',
  package='mediapipe',
  syntax='proto2',
  serialized_options=None,
  create_key=_descriptor._internal_create_key,
  serialized_pb=b'\n9mediapipe/calculators/video/box_detector_calculator.proto\x12\tmediapipe\x1a$mediapipe/framework/calculator.proto\x1a*mediapipe/util/tracking/box_detector.proto\"\xcd\x01\n\x1c\x42oxDetectorCalculatorOptions\x12\x37\n\x10\x64\x65tector_options\x18\x01 \x01(\x0b\x32\x1d.mediapipe.BoxDetectorOptions\x12\x1c\n\x14index_proto_filename\x18\x02 \x03(\t2V\n\x03\x65xt\x12\x1c.mediapipe.CalculatorOptions\x18\xe2\xdc\x94\x8a\x01 \x01(\x0b\x32\'.mediapipe.BoxDetectorCalculatorOptions'
  ,
  dependencies=[mediapipe_dot_framework_dot_calculator__pb2.DESCRIPTOR,mediapipe_dot_util_dot_tracking_dot_box__detector__pb2.DESCRIPTOR,])




_BOXDETECTORCALCULATOROPTIONS = _descriptor.Descriptor(
  name='BoxDetectorCalculatorOptions',
  full_name='mediapipe.BoxDetectorCalculatorOptions',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='detector_options', full_name='mediapipe.BoxDetectorCalculatorOptions.detector_options', index=0,
      number=1, type=11, cpp_type=10, label=1,
      has_default_value=False, default_value=None,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='index_proto_filename', full_name='mediapipe.BoxDetectorCalculatorOptions.index_proto_filename', index=1,
      number=2, type=9, cpp_type=9, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
  ],
  extensions=[
    _descriptor.FieldDescriptor(
      name='ext', full_name='mediapipe.BoxDetectorCalculatorOptions.ext', index=0,
      number=289746530, type=11, cpp_type=10, label=1,
      has_default_value=False, default_value=None,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=True, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
  ],
  nested_types=[],
  enum_types=[
  ],
  serialized_options=None,
  is_extendable=False,
  syntax='proto2',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=155,
  serialized_end=360,
)

_BOXDETECTORCALCULATOROPTIONS.fields_by_name['detector_options'].message_type = mediapipe_dot_util_dot_tracking_dot_box__detector__pb2._BOXDETECTOROPTIONS
DESCRIPTOR.message_types_by_name['BoxDetectorCalculatorOptions'] = _BOXDETECTORCALCULATOROPTIONS
_sym_db.RegisterFileDescriptor(DESCRIPTOR)

BoxDetectorCalculatorOptions = _reflection.GeneratedProtocolMessageType('BoxDetectorCalculatorOptions', (_message.Message,), {
  'DESCRIPTOR' : _BOXDETECTORCALCULATOROPTIONS,
  '__module__' : 'mediapipe.calculators.video.box_detector_calculator_pb2'
  # @@protoc_insertion_point(class_scope:mediapipe.BoxDetectorCalculatorOptions)
  })
_sym_db.RegisterMessage(BoxDetectorCalculatorOptions)

_BOXDETECTORCALCULATOROPTIONS.extensions_by_name['ext'].message_type = _BOXDETECTORCALCULATOROPTIONS
mediapipe_dot_framework_dot_calculator__options__pb2.CalculatorOptions.RegisterExtension(_BOXDETECTORCALCULATOROPTIONS.extensions_by_name['ext'])

# @@protoc_insertion_point(module_scope)
