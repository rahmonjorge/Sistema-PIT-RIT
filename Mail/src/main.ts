import * as grpc from "@grpc/grpc-js";
import {
  IEmailService,
  emailServiceDefinition,
} from "./protos/mail.grpc-server";
import nodemailer from "nodemailer";
import dotenv from "dotenv";
import { Type } from "./protos/mail";

dotenv.config();

interface GRPCError extends Error {
  code?: number;
}

const transporter = nodemailer.createTransport({
  service: "gmail",
  auth: {
    user: process.env.EMAIL_USERNAME,
    pass: process.env.EMAIL_PASSWORD,
  },
});

const getSubject = (type: Type) => {
  switch (type) {
    case Type.bug:
      return "[BUG 🐛] - Sistema PIT-RIT";
    case Type.feature:
      return "[SUGESTÃO 💡] - Sistema PIT-RIT";
    case Type.question:
      return "[PERGUNTA ❓] - Sistema PIT-RIT";
    default:
      return "[MENSAGEM 💬] - Sistema PIT-RIT";
  }
};

const emailService: IEmailService = {
  sendEmail: async (call, callback) => {
    const { from, name, message, type } = call.request;

    const mailBody = {
      messageFrom: "",
      body: message,
    };

    const subject = getSubject(type);

    if (type === Type.question) {
      if (from === undefined) {
        const error: GRPCError = new Error(
          "É necessário informar um e-mail para contato"
        );
        error.code = grpc.status.INVALID_ARGUMENT;
        callback(error, null);
        return;
      }
    }

    if (from === undefined) {
      if (name === undefined) {
        mailBody.messageFrom = "Mensagem enviada por um visitante";
      } else {
        mailBody.messageFrom = `Mensagem enviada por ${name}`;
      }
    } else {
      if (name === undefined) {
        mailBody.messageFrom = `Mensagem enviada por ${from}`;
      } else {
        mailBody.messageFrom = `Mensagem enviada por ${name} (${from})`;
      }
    }

    const html = `
        <h3>${mailBody.messageFrom}</h3>
        <p>${mailBody.body}</p>`;
    const amp = `
    <!DOCTYPE html>
    <html ⚡4email>
      <head>
        <meta charset="utf-8" />
        <style amp4email-boilerplate>
          body {
            visibility: hidden;
          }
        </style>
        <style amp-custom>
          * {
            box-sizing: border-box;
            font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
          }

          div.wrapper {
              width: 100%;
              height: 100%;
              background-color: #f1f1f1;
              border-radius: 0.75rem;
              margin: 0;
              padding: 2rem;
              display: flex;
              flex-direction: column;
              justify-content: center;
              gap: 1.5rem;
          }

          h1 {
              font-size: 1.5rem;
              font-weight: 500;
              color: #000;
              margin: 0;
              padding: 0;
          }

          p {
              font-size: 1rem;
              font-weight: 400;
              color: #000;
              margin: 0;
              padding: 0;
          }
        </style>
        <script async src="https://cdn.ampproject.org/v0.js"></script>
      </head>
      <body>
        <div class="wrapper">
          <h1>${mailBody.messageFrom}</h1>
          <p>${mailBody.body}</p>
        </div>  
      </body>
    </html>
    `;

    const mailOptions = {
      from:
        name === undefined
          ? `Sistema PIT-RIT <${process.env.EMAIL_USERNAME}>`
          : `${name} <${process.env.EMAIL_USERNAME}>`,
      to: "giansantos.dev@gmail.com",
      subject,
      html,
      amp,
    };

    const result = await transporter.sendMail(mailOptions);

    if (result.accepted.length > 0) {
      console.log("E-mail enviado com sucesso");
      callback(null, { success: true, message: "E-mail enviado com sucesso" });
    }
    if (result.rejected.length > 0) {
      const error: GRPCError = new Error("Erro ao enviar e-mail");
      error.code = grpc.status.INTERNAL;
      callback(error, null);
      return;
    }
  },
};

const server = new grpc.Server();
server.addService(emailServiceDefinition, emailService);
server.bindAsync(
  "0.0.0.0:5511",
  grpc.ServerCredentials.createInsecure(),
  (err) => {
    if (err) {
      console.error(err);
      throw err;
    }
    console.log("Server running at http://localhost:5511");
    server.start();
  }
);
